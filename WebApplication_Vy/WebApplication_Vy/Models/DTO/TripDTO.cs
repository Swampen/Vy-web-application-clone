using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Vy.Models.DTO
{
    public class TripDTO
    {
        [Key]
        public int TripId { get; set; }

        public string Route { get; set; }

        public int Price { get; set; }
    }
}