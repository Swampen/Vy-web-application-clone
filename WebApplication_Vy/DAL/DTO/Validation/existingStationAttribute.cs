using DAL.Db.Repositories.Contracts;
using DAL.Db.Repositories.Implementation;
using System.ComponentModel.DataAnnotations;

namespace DAL.DTO.Validation
{
    public class ExistingStationAttribute : ValidationAttribute
    {
        private readonly IStationRepository _repository = new StationRepositoryImpl();

        public override bool IsValid(object value)
        {
            var stations = _repository.FindAllStations();
            var station = (string)value;
            station = station.ToLower();

            foreach (var s in stations)
                if (s.Name.ToLower() == station)
                    return true;
            return false;
        }
    }
}