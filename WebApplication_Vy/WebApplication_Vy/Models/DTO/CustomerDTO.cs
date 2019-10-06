using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_Vy.Models.DTO
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "This field is required")]
        public string Givenname { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public ZipcodeDto Zipcode { get; set; }

        public List<TicketDto> Tickets { get; set; }
    }
}