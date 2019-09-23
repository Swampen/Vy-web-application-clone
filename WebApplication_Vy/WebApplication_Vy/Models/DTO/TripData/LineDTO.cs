using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication_Vy.Models.Entities;

namespace WebApplication_Vy.Models.DTO.TripData
{
    public class LineDTO
    {
        [Key]
        public int Id { get; set; }

        public virtual StationDTO Departure_Station { get; set; }

        public virtual StationDTO Arrival_Station { get; set; }
    }
}