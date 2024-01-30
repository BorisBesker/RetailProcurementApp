using Infrastructure.Data;
using Infrastructure.Models;

namespace Infrastructure.Repository
{
    public class StoreItemRepository : Repository<StoreItem> , IStoreItemRepository
    {
        public StoreItemRepository(RetailProcurementContext context)
            : base(context) { }

        public bool Exists(int id)
        {
            return Context.Set<StoreItem>().Any(x => x.Id == id);
        }

        public bool ExistsWithSameName(string name)
        {
            return Context.Set<StoreItem>().Any(x => x.ItemName.Trim().ToUpper() == name.Trim().Trim());
        }
    }
}
