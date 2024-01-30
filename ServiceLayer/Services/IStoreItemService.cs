using Infrastructure.Models;
using ServiceLayer.Models;

namespace ServiceLayer.Services
{
    public interface IStoreItemService
    {
        StoreItem? GetSpecificItem(int id);
        IEnumerable<StoreItem> GetStores();
        ServiceResponse<StoreItem> CreateItem(StoreItem item);
        ServiceResponse<StoreItem> UpdateItem(int id, StoreItem item);
        ServiceResponse<StoreItem> DeleteItem(int id);
    }
}