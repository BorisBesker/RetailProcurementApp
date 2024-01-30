namespace Infrastructure.Models
{
    public class SuplierStoreItem
    {
        public int StoreItemId { get; set; }
        public int SuplierId { get; set; }
        public decimal SuplierPrice { get; set; }
        public Suplier Suplier { get; set; } = null!;
        public StoreItem StoreItem { get; set; } = null!;
    }
}
