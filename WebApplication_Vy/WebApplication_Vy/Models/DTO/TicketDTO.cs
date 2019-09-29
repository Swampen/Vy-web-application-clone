using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using WebApplication_Vy.Models.DTO.TripData;

namespace WebApplication_Vy.Models.DTO
{
    public class TicketDTO
    {
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public String DepartureTime { get; set; }
        public String ArrivalTime { get; set; }
        public int Price { get; set; }
        public string Duration { get; set; }
        public string TrainChanges { get; set; }
    }
}