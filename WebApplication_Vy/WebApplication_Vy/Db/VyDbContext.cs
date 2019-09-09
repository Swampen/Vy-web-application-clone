﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WebApplication_Vy.Models.Entities;
using System.Xml.Linq;

namespace WebApplication_Vy.Db
{
    public class VyDbContext : DbContext
    {
        public VyDbContext() : base("name=VyDb")
        {
            Database.SetInitializer<VyDbContext>(new VyDbInitializer<VyDbContext>());
        }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public class VyDbInitializer<T> : DropCreateDatabaseAlways<VyDbContext>
        {
            protected override void Seed(VyDbContext context)
            {
                System.Diagnostics.Debug.WriteLine("got here");
                var xml = XElement.Load(HttpContext.Current.Server.MapPath("~") + "/routes.xml");
                System.Diagnostics.Debug.WriteLine(HttpContext.Current.Server.MapPath("~") + "routes.xml");
                var trips = xml.Descendants("Trip");
                System.Diagnostics.Debug.WriteLine(trips);

                foreach (var trip in trips)
                {
                    try { 
                       context.Trips.Add(new Trip()
                        {
                            Route = (string)trip.Element("Route"),
                            Price = (int)trip.Element("Price"),
                        });
                    }   catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine("XML config is wrong");
                    }
                }

            base.Seed(context);
            }
        }
    }
}