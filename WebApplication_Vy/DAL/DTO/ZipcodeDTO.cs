using System.ComponentModel.DataAnnotations;
using DAL.DTO.Validation;

namespace DAL.DTO
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