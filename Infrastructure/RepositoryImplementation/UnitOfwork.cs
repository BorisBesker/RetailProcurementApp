using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class UnitOfwork : IUnitOfWork
    {
        private readonly RetailProcurementContext _context;
        public UnitOfwork()
        {
            _context = new RetailProcurementContext();
            Supliers = new SuplierRepository(_context);
            StoreItems = new StoreItemRepository(_context);
        }

        public ISuplierRepository Supliers { get; private set; }
        public IStoreItemRepository StoreItems {  get; private set; }

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
