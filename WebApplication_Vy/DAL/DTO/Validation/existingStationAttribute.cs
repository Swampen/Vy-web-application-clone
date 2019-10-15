using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using DAL.Db.Repositories.Contracts;
using DAL.Db.Repositories.Implementation;

namespace DAL.DTO.Validation
{
    public class ExistingStationAttribute : ValidationAttribute
    {
        private readonly ITripRepository _repository = new TripRepositoryImpl();

        public override bool IsValid(object value)
        {
            var stations = _repository.FindAllStations();
            Debug.WriteLine(value);
            var station = (string) value;
            station = station.ToLower();

            foreach (var s in stations)
                if (s.Name.ToLower() == station)
                    return true;
            return false;
        }
    }
}