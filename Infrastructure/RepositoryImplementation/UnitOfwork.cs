using Infrastructure.Data;
using Infrastructure.RepositoryImplementation;

namespace Infrastructure.Repository
{
    public class UnitOfwork : IUnitOfWork
    {
        private readonly RetailProcurementContext _context;

        private ISuplierRepository _supliers;

        private IStoreItemRepository _storeItems;

        private ISuplierItemRepository _suplierItems;

        private IUserRepository _users;

        public ISuplierRepository Supliers => _supliers ?? (_supliers = new SuplierRepository(_context));
        public IStoreItemRepository StoreItems  => _storeItems ?? (_storeItems = new StoreItemRepository(_context));
        public ISuplierItemRepository SuplierItems => _suplierItems ?? (_suplierItems = new SuplierItemRepository(_context));
        public IUserRepository Users => _users ?? (_users = new UserRepository(_context));

        public UnitOfwork()
        {
            _context = new RetailProcurementContext();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
