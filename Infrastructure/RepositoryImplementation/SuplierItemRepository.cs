using Infrastructure.Data;
using Infrastructure.Models;
using Infrastructure.Repository;

namespace Infrastructure.RepositoryImplementation
{
    public class SuplierItemRepository : Repository<SuplierStoreItem>, ISuplierItemRepository
    {
        public SuplierItemRepository(RetailProcurementContext context)
            : base(context) { }
    }
}
