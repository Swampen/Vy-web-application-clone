using System;
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

        public DbSet<Zipcode> Zipcodes { get; set; }

        public class VyDbInitializer<T> : CreateDatabaseIfNotExists<VyDbContext>
        {
            protected override void Seed(VyDbContext context)
            {
                var tripXML = XElement.Load(HttpContext.Current.Server.MapPath("~/Content/") + "routes.xml");
                System.Diagnostics.Debug.WriteLine(HttpContext.Current.Server.MapPath("~/Content/") + "routes.xml");
                var trips = tripXML.Descendants("Trip");

                foreach (var trip in trips)
                {
                    try { 
                       context.Trips.Add(new Trip()
                        {
                            Route = (string)trip.Element("Route"),
                            Price = (int)trip.Element("Price"),
                        });
                    } catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine("XML config is wrong");
                    }
                }

                var zipxml = XElement.Load(HttpContext.Current.Server.MapPath("~/Content/") + "zipcodes.xml");
                System.Diagnostics.Debug.WriteLine(HttpContext.Current.Server.MapPath("~/Content/") + "zipcodes.xml");
                var zipz = zipxml.Descendants("Zipcode");

                foreach (var zipcode in zipz) {
                    try {
                        context.Zipcodes.Add(new Zipcode() {
                            Postalcode = (string)zipcode.Element("Postalcode"),
                            Postaltown = (string)zipcode.Element("Postaltown"),
                        });
                    } catch (Exception e) {
                        System.Diagnostics.Debug.WriteLine("XML config is wrong");
                    }
                }

                base.Seed(context);
            }
        }
    }
}