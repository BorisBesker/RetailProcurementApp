using Infrastructure.Data;
using Infrastructure.Models;

namespace Infrastructure.Repository
{
    public class StoreItemRepository : Repository<StoreItem> , IStoreItemRepository
    {
        public StoreItemRepository(RetailProcurementContext context)
            : base(context) { }
    }
}
