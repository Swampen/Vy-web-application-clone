using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_Vy.Models.DTO.TripData
{
    public class TripDTO
    {
        /*[Display(Name = "Route")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]*/
        [Key]
        public int Id { get; set; }

        public virtual LineDTO Line { get; set; }

        public virtual TripIntervalDTO TripInterval { get; set; }

        public virtual List<ScheduleDTO> Schedules { get; set; }

        public string Departure_Time { get; set; }

        public string Arrival_Time { get; set; }
    }
}