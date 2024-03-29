﻿using Infrastructure.Models;
using Infrastructure.Repository;
using ServiceLayer.Models;
using ServiceLayer.Services;

namespace ServiceLayer.ServicesImplementation
{
    public class SuplerItemsService : ISuplerItemsService
    {
        private readonly IUnitOfWork _database;

        public SuplerItemsService(IUnitOfWork database)
        {
            _database = database;
        }

        public IEnumerable<SuplierStoreItem> GetSuplierItemRelationships()
        {
            return _database.SuplierItems.GetAll();
        }

        public ServiceResponse<SuplierStoreItem> CreateSuplierItemRelationship(SuplierStoreItem suplierStoreItem)
        {
            if (_database.SuplierItems.Get(suplierStoreItem.StoreItemId, suplierStoreItem.SuplierId) != null)
            {
                return new ServiceResponse<SuplierStoreItem> { Success = false, RecordExists = true };
            }

            if (!_database.Supliers.Exists(suplierStoreItem.SuplierId) || !_database.StoreItems.Exists(suplierStoreItem.StoreItemId))
            {
                return new ServiceResponse<SuplierStoreItem> { Success = false, RelationShipEntityMissing = true };
            }

            _database.SuplierItems.Add(suplierStoreItem);

            _database.Save();

            return new ServiceResponse<SuplierStoreItem> { Success = true, Entity = suplierStoreItem };
        }

        public ServiceResponse<SuplierStoreItem> DeleteSuplierItemRelationship(int supplierId, int storeItemId)
        {
            var relationShip = _database.SuplierItems.Get(storeItemId, supplierId);

            if (relationShip == null)
            {
                return new ServiceResponse<SuplierStoreItem> { Success = false, RecordExists = false };
            }

            // DO not instanciate another instace for delete => EF find already tracks the instance 

            _database.SuplierItems.Remove(relationShip);

            _database.Save();

            return new ServiceResponse<SuplierStoreItem> { Success = true, RecordExists = true };
        }    
    }
}
