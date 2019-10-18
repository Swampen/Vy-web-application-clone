using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Entities
{
    public class Admin
    {
        public String Username { get; set; }
        public String Password { get; set; }
        public bool SuperUltra { get; set; }
        public int Id { get; set; }
    }
}
