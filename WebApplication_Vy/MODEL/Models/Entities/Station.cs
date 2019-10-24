using System.ComponentModel.DataAnnotations;

namespace MODEL.Models.Entities
{
    [TrackChanges]
    public class Station
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string StopId { get; set; }
    }
}
