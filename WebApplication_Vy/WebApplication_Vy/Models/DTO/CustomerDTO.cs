using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Vy.Models.DTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }

        public string Givenname { get; set; }

        public string Surname { get; set; }

        public string Address { get; set; }

        public virtual ZipcodeDTO ZipcodeDTO { get; set; }

        public virtual List<TicketDTO> Tickets { get; set; }

    }
}