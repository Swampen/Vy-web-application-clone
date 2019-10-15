using System.Collections.Generic;
using MODEL.Models.Entities;
using MODEL.Models.Entities.TripData;

namespace DAL.Db.Repositories.Contracts
{
    public interface ITripRepository
    {
        List<Trip> TripSearch(string query);
        List<Station> FindAllStations();
    }
}