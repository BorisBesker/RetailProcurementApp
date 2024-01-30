namespace Infrastructure.Repository
{
    public interface IUnitOfWork
    {
        ISuplierRepository Supliers { get; }

        IStoreItemRepository StoreItems { get; }

        ISuplierItemRepository SuplierItems { get; }

        IUserRepository Users { get; }

        int Save();
    }
}
