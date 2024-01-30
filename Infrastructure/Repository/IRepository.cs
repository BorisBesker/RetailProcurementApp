namespace Infrastructure.Repository
{
    public interface IRepository<TEntitiy> where TEntitiy : class
    {
        TEntitiy Get(params object?[] id);
        IEnumerable<TEntitiy> GetAll();
        void Add(TEntitiy entity);
        void Update(TEntitiy entity);
        void Remove(TEntitiy entity);
    }
}
