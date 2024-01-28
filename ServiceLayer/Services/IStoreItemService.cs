using Infrastructure.Models;
using ServiceLayer.Models;

namespace ServiceLayer.Services
{
    public interface IStoreItemService
    {
        StoreItem? GetSpecificStore(int id);
        IEnumerable<StoreItem> GetStores();
        StoreItemCreateResponse CreateItem(StoreItem item);
    }
}