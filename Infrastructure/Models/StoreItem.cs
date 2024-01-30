namespace Infrastructure.Models
{
    public class StoreItem
    {
        public int Id { get; set; }

        public string ItemName { get; set; }

        public string ItemDescription { get; set; }

        public decimal Price { get; set; }

        public List<Suplier> Supliers { get; } = [];

        public List<SuplierStoreItem> SuplierStoreItems { get; } = [];
    }
}
