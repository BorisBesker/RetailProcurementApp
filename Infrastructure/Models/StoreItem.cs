using System.Collections.ObjectModel;

namespace Infrastructure.Models
{

    public class StoreItem
    {
        public int Id { get; set; }

        public string ItemName { get; set; }

        public string ItemDescription { get; set; }

        public decimal Price { get; set; }

        //private List<Suplier> _supliers;

        //public ReadOnlyCollection<Suplier> Supliers => _supliers.AsReadOnly() ?? new List<Suplier>().AsReadOnly();

        public List<Suplier> Supliers { get; } = [];
    }
}
