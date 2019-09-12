using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Models.Entities;

namespace WebApplication_Vy.Db.Repositories.Implementation
{
    public class TripRepositoryImpl : ITripRepository
    {
        public List<Trip> FindAllTrips()
        {
            throw new NotImplementedException();
        }

        public List<Trip> TripSearch(string query)
        {
            using (var qVyDbContext = new VyDbContext())
            {
                if (query.Length >= 3)
                {
                    return qVyDbContext.Trips
                        .Where(trip => trip.Route.Contains(query))
                        .ToList();    
                }
                else
                {
                    return qVyDbContext.Trips
                        .Where(trip => trip.Route.StartsWith(query))
                        .ToList();
                }
            }
        }
    }
}