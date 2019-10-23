using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure.Design;

namespace DAL.DTO
{
    public class AdminUserDTO
    {
        public int Id{ get; set; }

        [Required(ErrorMessage = "Please fill in your username")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }
        
        public bool SuperAdmin { get; set; }
    }
}