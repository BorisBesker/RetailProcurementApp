namespace Infrastructure.Repository
{
    public interface IUnitOfWork
    {
        ISuplierRepository Supliers { get; }

        IStoreItemRepository StoreItems { get; }

        int Save();
    }
}
