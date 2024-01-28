using Infrastructure.Models;

namespace Infrastructure.Repository
{
    public interface IStoreItemRepository : IRepository<StoreItem>
    {
        public bool Exists(int id);
        public bool ExistsWithSameName(string name);
    }
}
