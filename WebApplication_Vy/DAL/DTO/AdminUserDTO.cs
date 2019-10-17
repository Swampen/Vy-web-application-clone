using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure.Design;

namespace DAL.DTO
{
    public class AdminUserDTO
    {
        public string Id { get; set; }
        
        public bool SuperUltra { get; set; }
        
        [Required(ErrorMessage = "Please fill in your email address")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }
    }
}