using System.ComponentModel.DataAnnotations;
using System.Runtime.Remoting.Services;

namespace MODEL.Models.Entities
{ 
    [TrackChanges]
    public class AdminUser
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] Password { get; set; }
        public string salt { get; set; }
        public bool SuperAdmin { get; set; }
    }
}