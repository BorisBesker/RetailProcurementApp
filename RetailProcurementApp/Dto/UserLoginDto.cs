using System.ComponentModel.DataAnnotations;

namespace RetailProcurementApp.Dto
{
    public class UserLoginDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
