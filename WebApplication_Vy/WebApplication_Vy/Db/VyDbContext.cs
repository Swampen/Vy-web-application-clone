using System;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Xml.Linq;
using WebApplication_Vy.Models.Entities;

namespace WebApplication_Vy.Db
{
    public class VyDbContext : DbContext
    {
        public VyDbContext() : base("name=VyDb")
        {
            Database.SetInitializer(new VyDbInitializer<VyDbContext>());
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Zipcode> Zipcodes { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }

        public class VyDbInitializer<T> : CreateDatabaseIfNotExists<VyDbContext>
        {
            protected override void Seed(VyDbContext context)
            {
                var zipxml = XElement.Load(HttpContext.Current.Server.MapPath("~/Content/") + "zipcodes.xml");
                Debug.WriteLine(HttpContext.Current.Server.MapPath("~/Content/") + "zipcodes.xml");
                var zipz = zipxml.Descendants("Zipcode");

                foreach (var zipcode in zipz)
                    try
                    {
                        context.Zipcodes.Add(new Zipcode
                        {
                            Postalcode = (string) zipcode.Element("Postalcode"),
                            Postaltown = (string) zipcode.Element("Postaltown")
                        });
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("XML config is wrong");
                        Console.WriteLine(e.StackTrace);
                        throw;
                    }
                
                var stationsXML = XElement.Load(HttpContext.Current.Server.MapPath("~/Content/") + "stations.xml");
                Debug.WriteLine(HttpContext.Current.Server.MapPath("~/Content/") + "stations.xml");
                var stations = stationsXML.Descendants("Station");

                foreach (var station in stations)
                    try
                    {
                        context.Stations.Add(new Station()
                        {
                            Name = (string)station.Element("Name")
                        });
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("XML config is wrong");
                        Console.WriteLine(e.StackTrace);
                        throw;
                    }
                
                try
                {
                    var data = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                        @"Content\valgdagen.csv"));
                    var reader = new StringReader(data);
                    reader.ReadLine();
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                        var strings = line.Split(';');
                        context.Stations.Add(new Station
                        {
                            Name = strings[0],
                            StopId = strings[1]
                        });
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                base.Seed(context);
            }
        }
    }
}