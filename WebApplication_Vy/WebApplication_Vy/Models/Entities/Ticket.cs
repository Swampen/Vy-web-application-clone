using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_Vy.Models.Entities
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public virtual Trip Trip { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Station Departure_Station { get; set; }
        public virtual Station Arrival_Station { get; set; }

    }
}