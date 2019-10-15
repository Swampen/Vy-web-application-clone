using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication_Vy.Models.Entities
{
    public class Line
    {
        [Key]
        public int Id { get; set; }

        public virtual Station Departure_Station { get; set; }

        public virtual Station Arrival_Station { get; set; }

    }
}