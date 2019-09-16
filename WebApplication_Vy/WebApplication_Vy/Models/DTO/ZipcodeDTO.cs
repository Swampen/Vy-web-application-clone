using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication_Vy.Models.DTO
{
    public class ZipcodeDTO
    {
        [Key]
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("[0-9]{4}", ErrorMessage ="Postal code must be 4 numbers")]
        public string Postalcode { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Postaltown { get; set; }
    }
}