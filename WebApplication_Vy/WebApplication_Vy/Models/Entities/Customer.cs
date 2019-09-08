using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Vy.Models.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string Givenname { get; set; }

        public string Surname { get; set; }

        public string Address { get; set; }

        public int Zipcode { get; set; }
    }
}