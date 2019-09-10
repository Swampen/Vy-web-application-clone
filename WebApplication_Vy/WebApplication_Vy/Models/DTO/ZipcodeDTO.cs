using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication_Vy.Models.DTO
{
    public class ZipcodeDTO
    {
        [Key]
        public string Postalcode { get; set; }

        public string Postaltown { get; set; }
    }
}