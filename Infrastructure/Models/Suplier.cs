using Azure;
using System.Collections.ObjectModel;

namespace Infrastructure.Models
{
    public class Suplier
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public  string Country { get; set; }

        //private List<StoreItem> _storeItems;

        //public ReadOnlyCollection<StoreItem> StoreItems => _storeItems.AsReadOnly();

        public List<StoreItem> StoreItems { get; } = [];
    }
}
