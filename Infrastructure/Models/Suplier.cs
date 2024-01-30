namespace Infrastructure.Models
{
    public class Suplier
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public  string Country { get; set; }

        public List<StoreItem> StoreItems { get; } = [];

        public List<SuplierStoreItem> SuplierStoreItems { get; } = [];
    }
}
