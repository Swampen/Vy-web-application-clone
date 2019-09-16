using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_Vy.Models.DTO
{
    public class TripDTO
    {
        [Key]
        [Display(Name = "Route")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public int TripId { get; set; }

        public string Route { get; set; }

        public int Price { get; set; }
    }
}