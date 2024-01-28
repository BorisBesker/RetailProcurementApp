using Infrastructure.Models;
using Infrastructure.Repository;
using ServiceLayer.Models;

namespace ServiceLayer.Services
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

        public StoreItem? GetSpecificStore(int id)
        {
            if (!_database.StoreItems.Exists(id))
            {
                return null;
            }
            return _database.StoreItems.Get(id); 
        }

        public StoreItemCreateResponse CreateItem(StoreItem item)
        {
            if (_database.StoreItems.ExistsWithSameName(item.ItemName))
            {
                return new StoreItemCreateResponse {ExistSameName = true };
            }

            _database.StoreItems.Add(item);

            _database.Save();

            return new StoreItemCreateResponse { ExistSameName = false, StoreItem = item };         
        }
    }
}
