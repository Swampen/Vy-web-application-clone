using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Models.Entities;

namespace WebApplication_Vy.Db.Repositories.Implementation
{
    public class TripRepositoryImpl : ITripRepository
    {
        public List<Station> FindAllStations()
        {
            using (var db = new VyDbContext())
            {
                return db.Stations.ToList();
            }
        }

        public List<Trip> FindAllTrips()
        {
            using (var qVyDbContext = new VyDbContext())
            {
                return qVyDbContext.Trips.ToList();
            }
        }

        public List<Trip> TripSearch(string query)
        {
            return new List<Trip>();
        }
    }
}