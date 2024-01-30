using System.ComponentModel.DataAnnotations;

namespace RetailProcurementApp.Dto
{
    public class SuplierDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
