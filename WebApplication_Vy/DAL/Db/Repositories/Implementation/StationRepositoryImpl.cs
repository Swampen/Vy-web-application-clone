using System.Collections.Generic;
using System.Linq;
using DAL.Db.Repositories.Contracts;
using MODEL.Models.Entities;

namespace DAL.Db.Repositories.Implementation
{
    public class StationRepositoryImpl : IStationRepository
    {
        public List<Station> FindAllStations()
        {
            using (var db = new VyDbContext())
            {
                return db.Stations.ToList();
            }
        }
    }
}