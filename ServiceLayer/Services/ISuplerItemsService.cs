using Infrastructure.Models;
using ServiceLayer.Models;

namespace ServiceLayer.Services
{
    public interface ISuplerItemsService
    {
        public IEnumerable<SuplierStoreItem> GetSuplierItemRelationships();
        public ServiceResponse<SuplierStoreItem> CreateSuplierItemRelationship(SuplierStoreItem suplierStoreItem);
    }
}
