using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_Vy.Models.Entities
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }
        public virtual Line Line { get; set; }
        public virtual TripInterval TripInterval { get; set; }
        public string Departure_Time { get; set; }
        public string Arrival_Time { get; set; }
    }
}