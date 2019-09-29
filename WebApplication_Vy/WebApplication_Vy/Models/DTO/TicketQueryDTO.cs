using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Vy.Models.DTO
{
    public class TicketQueryDTO
    {
        public int Id { get; set; }
        public string Departure { get; set; }

        public bool Roundtrip { get; set; }

        public string CSname { get; set; }

        public string CGname { get; set; }

        public string Address { get; set; }

        public string Zipcode { get; set; }

    }
}