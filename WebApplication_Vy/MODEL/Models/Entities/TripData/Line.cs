using System.ComponentModel.DataAnnotations;

namespace MODEL.Models.Entities.TripData
{
    public class Line
    {
        [Key]
        public int Id { get; set; }

        public virtual Station Departure_Station { get; set; }

        public virtual Station Arrival_Station { get; set; }

    }
}