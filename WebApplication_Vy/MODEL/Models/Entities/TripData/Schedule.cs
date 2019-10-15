using System.ComponentModel.DataAnnotations;

namespace MODEL.Models.Entities.TripData
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        public virtual Trip Trip { get; set; }

        public virtual Station Station { get; set; }

        public string Arrival_Time { get; set; }

        public string Departure_Time { get; set; }
    }
}