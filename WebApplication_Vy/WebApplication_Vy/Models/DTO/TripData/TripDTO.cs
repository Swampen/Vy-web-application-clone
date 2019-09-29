using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_Vy.Models.DTO.TripData
{
    public class TripDTO
    {
        public string Departure_Station { get; set; }
        public string Arrival_Station { get; set; }
        public string Date { get; set; }
        public string Departure_Time { get; set; }
        public string Arrival_Time { get; set; }
        public Dictionary<string, int> Duration { get; set; }
        public string Train_Changes { get; set; }
        public int Price { get; set; }
        public string PriceType { get; set; }
    }
}