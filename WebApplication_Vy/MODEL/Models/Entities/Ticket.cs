﻿using System.ComponentModel.DataAnnotations;

namespace MODEL.Models.Entities
{
    [TrackChanges]
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public int Price { get; set; }
        public string Duration { get; set; }
        public string TrainChanges { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual CreditCard CreditCard { get; set; }
    }
}