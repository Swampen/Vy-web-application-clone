using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication_Vy.Models.Entities
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public virtual Trip Trip { get; set; }
        public virtual Station Station { get; set; }
        public string Arrival_Time { get; set; }
        public string Departure_Time { get; set; }
    }
}