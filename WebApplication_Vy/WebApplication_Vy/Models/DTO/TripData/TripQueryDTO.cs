using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Vy.Models.DTO.TripData
{
    public class TripQueryDTO
    {
        public List<string> Departure_Station { get; set; }
        
        public List<string> Arrival_Station { get; set; }

        public List<string> Date { get; set; }

        public List<string> Time { get; set; }
        public bool Round_Trip { get; set; }
    }
}