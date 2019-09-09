using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WebApplication_Vy.Models.Entities;

namespace WebApplication_Vy.Db
{
    public class VyDbContext : DbContext
    {
        public VyDbContext()
            : base("name=VyDb")
        {
            Database.CreateIfNotExists();
        }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Trip> Trips { get; set; }
    }
}