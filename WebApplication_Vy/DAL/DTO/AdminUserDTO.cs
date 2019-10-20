using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure.Design;

namespace DAL.DTO
{
    public class AdminUserDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Please fill in your email address")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }
        
        public bool SuperAdmin { get; set; }
    }
}