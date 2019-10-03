using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication_Vy.Models.DTO.TripData
{
    public class TripQueryDTO
    {
        [Display(Name = "From")]
        [Required(ErrorMessage = "This field is required")]
        public string Departure_Station { get; set; }
        
        [Display(Name = "To")]
        [Required(ErrorMessage = "This field is required")]
        public string Arrival_Station { get; set; }

        [Display(Name = "Departure date")]
        [Required(ErrorMessage = "This field is required")]
        public string Date { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Time { get; set; }

        [Display(Name = "Return date")]
        public bool Round_Trip { get; set; }

        [Display(Name = "Time")]
        public string Return_Date { get; set; }

        public string Return_Time { get; set; }
        
        public int Adult { get; set; }
        [Display(Name = "Child 6-17 years old")]
        public int Child { get; set; }
        public int Senior { get; set; }
        public int Student { get; set; }

    }
}