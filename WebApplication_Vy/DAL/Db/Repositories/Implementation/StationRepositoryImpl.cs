using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Db.Repositories.Contracts;
using MODEL.Models.Entities;
using UTILS.Utils.Logging;

namespace DAL.Db.Repositories.Implementation
{
    public class StationRepositoryImpl : IStationRepository
    {
        private static readonly log4net.ILog Log = LogHelper.GetLogger();
        public List<Station> FindAllStations()
        {
            using (var db = new VyDbContext())
            {
                try
                {
                    List<Station> stations = db.Stations.ToList();
                    return stations.OrderBy(s => s.Name).ToList();
                }
                catch (Exception e)
                {
                    Log.Error(LogEventPrefixes.DATABASE_ERROR + e.Message, e);
                    throw;
                }
            }
        }
    }
}