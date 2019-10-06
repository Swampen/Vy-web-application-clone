using System.ComponentModel.DataAnnotations;
using WebApplication_Vy.Models.DTO.Validation;

namespace WebApplication_Vy.Models.DTO
{
    public class ZipcodeDto
    {
        [Key]
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("[0-9]{4}", ErrorMessage = "Postal code must be 4 numbers")]
        [Zipcode(ErrorMessage = "Not a valid Norwegian zipcode")]
        public string Postalcode { get; set; }

        public string Postaltown { get; set; }
    }
}