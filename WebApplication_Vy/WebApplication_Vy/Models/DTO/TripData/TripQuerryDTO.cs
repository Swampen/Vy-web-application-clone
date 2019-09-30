using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Vy.Models.DTO.TripData
{
    public class TripQuerryDTO
    {
        public string Departure_Station { get; set; }
        
        public string Arrival_Station { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }
        public bool Round_Trip { get; set; }
        public string Return_Date { get; set; }
        public string Return_Time { get; set; }
    }
}