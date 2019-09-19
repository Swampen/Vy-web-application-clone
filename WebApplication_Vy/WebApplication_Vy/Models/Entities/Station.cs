using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication_Vy.Models.Entities
{
    public class Station
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}