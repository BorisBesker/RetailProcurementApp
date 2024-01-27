using Azure;

namespace Infrastructure.Models
{
    public class Suplier
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Address { get; set; }

        public required string Country { get; set; }

        public List<StoreItem> StoreItems { get; } = [];
    }
}
