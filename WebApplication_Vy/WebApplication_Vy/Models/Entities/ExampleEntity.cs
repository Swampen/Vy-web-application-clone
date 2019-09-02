using System.ComponentModel.DataAnnotations;

namespace WebApplication_Vy.Models.Entities
{
    public class ExampleEntity
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
}