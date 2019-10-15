using System.Collections.Generic;
using System.Linq;
using DAL.Db.Repositories.Contracts;
using MODEL.Models.Entities;
using MODEL.Models.Entities.TripData;

namespace DAL.Db.Repositories.Implementation
{
    public class TripRepositoryImpl : ITripRepository
    {
        public List<Trip> TripSearch(string query)
        {
            return new List<Trip>();
        }

        public List<Station> FindAllStations()
        {
            var db = new VyDbContext();
            return Enumerable.ToList<Station>(db.Stations);
        }
    }
}