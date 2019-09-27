using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
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

        public DbSet<Trip> Trips { get; set; }

        public DbSet<Zipcode> Zipcodes { get; set; }

        public DbSet<Line> Lines { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<Station> Stations { get; set; }

        public DbSet<TripInterval> TripIntervals{ get; set; }


        public class VyDbInitializer<T> : CreateDatabaseIfNotExists<VyDbContext>
        {
            protected override void Seed(VyDbContext context)
            {
                var XML = XElement.Load(HttpContext.Current.Server.MapPath("~/Content/") + "trips.xml");
                Debug.WriteLine(HttpContext.Current.Server.MapPath("~/Content/") + "trips.xml");
                var stationsXML = XML.Element("Stations").Descendants("Name");

                List<Station> stations = new List<Station>();
                foreach (var s in stationsXML)
                {
                    Station station = new Station()
                    {
                        Name = (string)s,
                        Schedules = new List<Schedule>()
                    };
                    stations.Add(station);
                }

                var tripsXML = XML.Element("Trips");

                foreach (var tripXML in tripsXML.Descendants("Trip"))
                { 
                    var lineXML = tripXML.Element("Line");
                    var tripIntervalXML = tripXML.Element("TripInterval");
                    var schedulesXML = tripXML.Element("Schedules").Descendants("Schedule");

                    Line line = new Line();
                    List<Schedule> schedules = new List<Schedule>();

                    foreach (var station in stations)
                    {
                        if (station.Name == (string)lineXML.Element("Departure_Station"))
                        {
                            line.Departure_Station = station;
                        }else if(station.Name == (string)lineXML.Element("Arrival_Station"))
                        {
                            line.Arrival_Station = station;
                        }

                        foreach (var sch in schedulesXML)
                        {
                            if (station.Name == (string)sch.Element("Station").Element("Name"))
                            {
                                Schedule schedule = new Schedule()
                                {
                                    Arrival_Time = (string)sch.Element("Arrival_Time"),
                                    Departure_Time = (string)sch.Element("Departure_Time"),
                                    Station = station,
                                };
                                station.Schedules.Add(schedule);
                                schedules.Add(schedule);
                                break;
                            }
                        }
                    }

                    TripInterval tripInterval = new TripInterval()
                    {
                        Monday = (bool)tripIntervalXML.Element("Monday"),
                        Tuesday = (bool)tripIntervalXML.Element("Tuesday"),
                        Wednesday = (bool)tripIntervalXML.Element("Wednesday"),
                        Thursday = (bool)tripIntervalXML.Element("Thursday"),
                        Friday = (bool)tripIntervalXML.Element("Friday"),
                        Saturday = (bool)tripIntervalXML.Element("Saturday"),
                        Sunday = (bool)tripIntervalXML.Element("Sunday"),
                    };

                    Trip trip = new Trip()
                    {
                        Line = line,
                        TripInterval = tripInterval,
                        Arrival_Time = (string)tripXML.Element("Arrival_Time"),
                        Departure_Time = (string)tripXML.Element("Departure_Time"),
                        Schedules = schedules,
                    };

                    try
                    {
                        context.Trips.Add(trip);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e);
                    }

                    foreach (var s in stations)
                    { 
                        try
                        { 
                            context.Stations.Add(s);
                        }catch (Exception e)
                        {
                            Debug.WriteLine(e);
                        }
                    }
                }



                //var zipxml = XElement.Load(HttpContext.Current.Server.MapPath("~/Content/") + "zipcodes.xml");
                //Debug.WriteLine(HttpContext.Current.Server.MapPath("~/Content/") + "zipcodes.xml");
                //var zipz = zipxml.Descendants("Zipcode");

                //foreach (var zipcode in zipz)
                //    try
                //    {
                //        context.Zipcodes.Add(new Zipcode
                //        {
                //            Postalcode = (string) zipcode.Element("Postalcode"),
                //            Postaltown = (string) zipcode.Element("Postaltown")
                //        });
                //    }
                //    catch (Exception e)
                //    {
                //        Debug.WriteLine("XML config is wrong");
                //        Console.WriteLine(e.StackTrace);
                //        throw;
                //    }

                base.Seed(context);
            }
        }

        public System.Data.Entity.DbSet<WebApplication_Vy.Models.DTO.TicketDTO> TicketDTOes { get; set; }
    }
}