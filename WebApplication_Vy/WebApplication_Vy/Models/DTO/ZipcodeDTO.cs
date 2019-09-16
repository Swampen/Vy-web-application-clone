using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication_Vy.Models.DTO.Validation;

namespace WebApplication_Vy.Models.DTO
{
    public class ZipcodeDTO
    {
        [Key]
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("[0-9]{4}", ErrorMessage ="Postal code must be 4 numbers")]
        [Zipcode(ErrorMessage = "Not a valid Norwegian zipcode")]
        public string Postalcode { get; set; }

        //[Required(ErrorMessage = "Not a valid Norwegian zipcode")]
        public string Postaltown { get; set; }
    }
}