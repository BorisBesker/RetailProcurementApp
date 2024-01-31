using Infrastructure.Models;
using Infrastructure.Repository;
using ServiceLayer.Models;
using ServiceLayer.Services;

namespace ServiceLayer.ServicesImplementation
{
    public class Suplierservice : ISuplierService
    {
        private readonly IUnitOfWork _database;

        public Suplierservice(IUnitOfWork database)
        {
            _database = database;
        }

        public IEnumerable<Suplier> GetSupliers()
        {
            return _database.Supliers.GetAll();
        }

        public Suplier? GetSpecificSuplier(int id)
        {
            if (!_database.Supliers.Exists(id))
            {
                return null;
            }
            return _database.Supliers.Get(id);
        }

        public ServiceResponse<Suplier> CreateSuplier(Suplier suplier)
        {
            if (_database.Supliers.ExistsWithSameName(suplier.Name))
            {
                return new ServiceResponse<Suplier> { Success = false, ExistSameName = true };
            }

            _database.Supliers.Add(suplier);

            _database.Save();

            return new ServiceResponse<Suplier> { Success = true, ExistSameName = false, Entity = suplier };
        }

        public ServiceResponse<Suplier> UpdateSuplier(int id, Suplier suplier)
        {
            if (!_database.Supliers.Exists(id))
            {
                return new ServiceResponse<Suplier> { Success = false, RecordExists = false };
            }

            suplier.Id = id;

            _database.Supliers.Update(suplier);

            _database.Save();

            return new ServiceResponse<Suplier> { Success = true, RecordExists = true, Entity = suplier };
        }

        public ServiceResponse<Suplier> DeleteSuplier(int id)
        {
            if (!_database.Supliers.Exists(id))
            {
                return new ServiceResponse<Suplier> { Success = false, RecordExists = false };
            }

            var suplier = new Suplier
            {
                Id = id
            };

            _database.Supliers.Remove(suplier);

            _database.Save();

            return new ServiceResponse<Suplier> { Success = true, RecordExists = true, Entity = suplier };
        }
    }
}
