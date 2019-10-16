using System;
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
                try
                {
                    return db.Stations.ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
            }
        }
    }
}