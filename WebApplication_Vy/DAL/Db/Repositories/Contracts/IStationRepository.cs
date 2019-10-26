using MODEL.Models.Entities;
using System.Collections.Generic;

namespace DAL.Db.Repositories.Contracts
{
    public interface IStationRepository
    {
        List<Station> FindAllStations();
        bool UpdateStation(Station station);
        bool DeleteStation(int stationId);
        bool CreateStation(Station station);

    }
}