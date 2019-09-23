using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication_Vy.Models.DTO.TripData
{
    public class ScheduleDTO
    {
        [Key]
        public int Id { get; set; }

        public virtual TripDTO Trip { get; set; }

        public virtual StationDTO Station { get; set; }

        public string Arrival_Time { get; set; }

        public string Departure_Time { get; set; }
    }
}