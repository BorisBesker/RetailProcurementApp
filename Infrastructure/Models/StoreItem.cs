using Azure;

namespace Infrastructure.Models
{

    public class StoreItem
    {
        public int Id { get; set; }

        public required string ItemName { get; set; }

        public required string ItemDescription { get; set; }

        public decimal Price { get; set; }

        public List<Suplier> Supliers { get; } = [];
    }
}
