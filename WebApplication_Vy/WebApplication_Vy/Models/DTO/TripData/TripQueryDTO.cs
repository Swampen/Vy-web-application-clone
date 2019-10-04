using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication_Vy.Models.DTO.Validation;

namespace WebApplication_Vy.Models.DTO.TripData
{
    public class TripQueryDTO
    {
        [Display(Name = "From")]
        [Required(ErrorMessage = "This field is required")]
        [ExistingStation(ErrorMessage = "Invalid departure station")]
        public string Departure_Station { get; set; }
        
        [Display(Name = "To")]
        [Required(ErrorMessage = "This field is required")]
        [ExistingStation(ErrorMessage = "Invalid arrival station")]
        public string Arrival_Station { get; set; }

        [Display(Name = "Departure date")]
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("[0-9]{4}-[0-9]{2}-[0-9]{2}$", ErrorMessage = "Incorrect date format")]
        public string Date { get; set; }

        [Display(Name = "Time")]
        [Required(ErrorMessage = "This field is required")]
        public string Time { get; set; }

        public bool Round_Trip { get; set; }
        
        [Display(Name = "Return date")]
        public string Return_Date { get; set; }

        [Display(Name = "Return time")]
        public string Return_Time { get; set; }
        
        public int Adult { get; set; }
        [Display(Name = "Child 6-17 years old")]
        public int Child { get; set; }
        public int Senior { get; set; }
        public int Student { get; set; }

    }
}