using System.Collections.Generic;

namespace MODEL.Models.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string Givenname { get; set; }

        public string Surname { get; set; }
        public string Email { get; set; }

        public string Address { get; set; }

        public virtual Zipcode Zipcode { get; set; } 

        public virtual List<Ticket> Tickets { get; set; }
    }
}