using System.ComponentModel.DataAnnotations;

namespace RetailProcurementApp.Dto
{
    public class SuplierItemIdDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int StoreItemId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int SuplierId { get; set; }

        [Required]
        [Range(1, (double)decimal.MaxValue)]
        public decimal SuplierPrice { get; set; }
    }
}
