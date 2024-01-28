using Infrastructure.Models;

namespace ServiceLayer.Models
{
    public class StoreItemCreateResponse
    {
        public bool ExistSameName { get; set; }
        public StoreItem? StoreItem { get; set; }
    }
}
