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
        public int TicketNumber { get; set; }

        public DateTime Departure { get; set; }

        public bool Roundtrip { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Trip Trip { get; set; }

    }
}