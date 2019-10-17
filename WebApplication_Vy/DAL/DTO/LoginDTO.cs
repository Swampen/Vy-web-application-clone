using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    class LoginDTO
    {

        public AdminUserDTO Admin { get; set; }
        public String CurrentLocation { get; set; }
    }
}
