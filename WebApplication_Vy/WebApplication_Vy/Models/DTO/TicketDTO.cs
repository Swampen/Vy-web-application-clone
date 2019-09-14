using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_Vy.Models.DTO
{
    public class TicketDTO
    {
        [Key]
        public int TicketNumber { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public DateTime Departure { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public bool Roundtrip { get; set; }

        public CustomerDTO CustomerDTO { get; set; }

        public TripDTO TripDTO { get; set; }
    }
}