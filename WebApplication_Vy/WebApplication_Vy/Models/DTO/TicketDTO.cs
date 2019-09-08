using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Vy.Models.DTO
{
    public class TicketDTO
    {
        [Key]
        public int TicketNumber { get; set; }

        public DateField Departure { get; set; }

        public bool Roundtrip { get; set; }

        public Customer customer { get; set; }

        public Trip trip { get; set; }
    }
}