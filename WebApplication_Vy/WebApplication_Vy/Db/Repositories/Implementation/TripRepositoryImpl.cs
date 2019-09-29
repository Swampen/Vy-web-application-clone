using System;
using System.Collections.Generic;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Models.Entities;

namespace WebApplication_Vy.Db.Repositories.Implementation
{
    public class TripRepositoryImpl : ITripRepository
    {
        //TODO: This class can be deleted if it is not to be used
        public List<Station> FindAllStations()
        {
/*            using (var db = new VyDbContext())
            {
                return db.Stations.ToList();
            }*/
            throw new NotImplementedException();
        }

        public List<Trip> FindAllTrips()
        {
            /*using (var qVyDbContext = new VyDbContext())
            {
                return qVyDbContext.Trips.ToList();
            }*/
            throw new NotImplementedException();
        }

        public List<Trip> TripSearch(string query)
        {
            return new List<Trip>();
        }
    }
}