using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Vy.Db.Repositories.Implementation
{
    public class VyRepositoryImpl : VyRepository
    {
        public List<Ticket> findAll()
        {
            var db = new VyDbContext();
            return db.Tickets.ToList();
        }

        public List<Trip> findAll()
        {
            var db = new VyDbContext();
            return db.Trips.ToList();
        }

        public List<Customer> findAll()
        {
            var db = new VyDbContext();
            return db.Customers.ToList();
        }
    }
}