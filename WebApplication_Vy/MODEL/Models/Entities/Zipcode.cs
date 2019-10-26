using System.ComponentModel.DataAnnotations;

namespace MODEL.Models.Entities
{
    [TrackChanges]
    public class Zipcode
    {
        [Key]
        public string Postalcode { get; set; }

        public string Postaltown { get; set; }
    }
}