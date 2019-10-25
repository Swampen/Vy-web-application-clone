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

        public bool CreateStation(Station station)
        {
            var db = new VyDbContext();

            var foundStation = db.Stations.FirstOrDefault(s => s.StopId == station.StopId);
            if (foundStation == null)
            {
                try
                {
                    db.Stations.Add(station);
                    db.SaveChanges();
                    Log.Info(LogEventPrefixes.DATABASE_ACCESS +
                             "Created new station: " + station.Name);
                    return true;
                }
                catch (Exception e)
                {
                    Log.Error(LogEventPrefixes.DATABASE_ERROR + e.Message, e);
                    return false;
                }
            }
            return false;
        }

        public bool DeleteStation(int stationId)
        {
            var db = new VyDbContext();
            try
            {
                var station = db.Stations.Find(stationId);
                if (station != null)
                {
                    db.Stations.Remove(station);
                    db.SaveChanges();
                    Log.Info(LogEventPrefixes.DATABASE_ACCESS +
                             "Deleted station: " + station.Name);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Log.Error(LogEventPrefixes.DATABASE_ERROR + e.Message, e);
                return false;
            }
        }

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

        public bool UpdateStation(Station station)
        {
            var db = new VyDbContext();
            try
            {
                var foundStation = db.Stations.Find(station.Id);
                if (foundStation != null)
                {
                    foundStation.Name = station.Name;
                    db.SaveChanges();
                    Log.Info(LogEventPrefixes.DATABASE_ACCESS + "Updated name on stationID " + foundStation.Id + " to " + foundStation.Id);
                    return true;
                }
                return false;

            }
            catch (Exception e)
            {
                Log.Error(LogEventPrefixes.DATABASE_ERROR + e.Message, e);
                return false;
            }
        }
    }
}