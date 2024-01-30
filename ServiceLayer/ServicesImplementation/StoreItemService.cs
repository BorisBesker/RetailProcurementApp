using Infrastructure.Models;
using Infrastructure.Repository;
using ServiceLayer.Models;
using ServiceLayer.Services;

namespace ServiceLayer.ServicesImplementation
{
    public class StoreItemService : IStoreItemService
    {
        private readonly IUnitOfWork _database;

        public StoreItemService(IUnitOfWork database)
        {
            _database = database;
        }

        public IEnumerable<StoreItem> GetStores()
        {
            return _database.StoreItems.GetAll();
        }

        public StoreItem? GetSpecificItem(int id)
        {
            if (!_database.StoreItems.Exists(id))
            {
                return null;
            }
            return _database.StoreItems.Get(id);
        }

        public ServiceResponse<StoreItem> CreateItem(StoreItem item)
        {
            if (_database.StoreItems.ExistsWithSameName(item.ItemName))
            {
                return new ServiceResponse<StoreItem> { Success = false, ExistSameName = true };
            }

            _database.StoreItems.Add(item);

            _database.Save();

            return new ServiceResponse<StoreItem> { Success = true, ExistSameName = false, Entity = item };
        }

        public ServiceResponse<StoreItem> UpdateItem(int id, StoreItem item)
        {
            if (!_database.StoreItems.Exists(id))
            {
                return new ServiceResponse<StoreItem> { Success = false, RecordExists = false };
            }

            item.Id = id;

            _database.StoreItems.Update(item);

            _database.Save();

            return new ServiceResponse<StoreItem> { Success = true, RecordExists = true, Entity = item };
        }

        public ServiceResponse<StoreItem> DeleteItem(int id)
        {
            if (!_database.StoreItems.Exists(id))
            {
                return new ServiceResponse<StoreItem> { Success = false, RecordExists = false };
            }

            var storeItem = new StoreItem
            {
                Id = id
            };

            _database.StoreItems.Remove(storeItem);

            _database.Save();

            return new ServiceResponse<StoreItem> { Success = true, RecordExists = true, Entity = storeItem };
        }
    }
}
