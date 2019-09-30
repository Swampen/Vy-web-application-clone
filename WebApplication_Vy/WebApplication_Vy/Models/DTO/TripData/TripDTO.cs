﻿using System;
using System.Collections.Generic;

namespace WebApplication_Vy.Models.DTO.TripData
{
    public class TripDTO
    {
        public string Departure_Station { get; set; }
        public string Arrival_Station { get; set; }
        public string Date { get; set; }
        public string Departure_Time { get; set; }
        public string Arrival_Time { get; set; }
        public string Duration { get; set; }
        public string Train_Changes { get; set; }
        public int Price { get; set; }
        public string PriceType { get; set; }
        public bool returnTrip { get; set; }
        public string ReturnDate { get; set; }
        public string Return_Departure_Time { get; set; }
        public string Return_Arrival_Time { get; set; }
        public int ReturnPrice { get; set; }
    }
}