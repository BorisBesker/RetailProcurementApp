using System.ComponentModel.DataAnnotations;

namespace RetailProcurementApp.Dto
{
    public class StoreItemDto
    {
        [Required]
        public string ItemName { get; set; }

        [Required]
        public string ItemDescription { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
