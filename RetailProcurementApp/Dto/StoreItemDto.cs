using System.ComponentModel.DataAnnotations;

namespace RetailProcurementApp.Dto
{
    public class StoreItemDto
    {
        [Required]
        public string ItemName { get; set; }

        [Required]
        public string ItemDescription { get; set; }

        public decimal Price { get; set; }
    }
}
