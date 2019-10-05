using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Db.Repositories.Implementation;
using WebApplication_Vy.Models.Entities;

namespace WebApplication_Vy.Models.DTO.Validation
{
    public class ExistingStationAttribute : ValidationAttribute
    {
        private readonly ITripRepository _repository = new TripRepositoryImpl();

        public override bool IsValid(object value)
        {
            var stations = _repository.FindAllStations();
            Debug.WriteLine(value);
            var station = (string)value;

            foreach(var s in stations)
                if (s.Name == station)
                    return true;
            return false;
        }
    }
}