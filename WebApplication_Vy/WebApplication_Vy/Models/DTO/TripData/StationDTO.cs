using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication_Vy.Models.DTO.TripData
{
    public class StationDTO
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<ScheduleDTO> Schedules { get; set; }
    }
}