using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Vy.Models.Entities;

namespace WebApplication_Vy.Db.Repositories.Implementation
{
    public class VyRepositoryImpl : WebApplication_Vy.Db.Repositories.Contracts.IVyRepository
    {
        public List<Ticket> findAllTickets()
        {
            var db = new VyDbContext();
            return db.Tickets.ToList();
        }

        public List<Trip> findAllTrips()
        {
            var db = new VyDbContext();
            Customer t = db.Customers.Find(5);
            //db.Customers.Remove(t);
            //db.Tickets.Remove(t);
            //db.SaveChanges();
            return db.Trips.ToList();
        }

        public List<Customer> findAllCustomers()
        {
            var db = new VyDbContext();
            return db.Customers.ToList();
        }

        public Zipcode findZipcode(string postalcode)
        {
            var db = new VyDbContext();
            Zipcode zipcode = db.Zipcodes.FirstOrDefault(zip => zip.Postalcode == postalcode);
            return zipcode;
        }

        public bool createTicket(Ticket inTicket)
        {
            using (var db = new VyDbContext())
            {
                var tempTrip = db.Trips.Find(inTicket.Trip.TripId);

                var ticket = new Ticket()
                {
                    Departure = inTicket.Departure,
                    Roundtrip = inTicket.Roundtrip,
                    Trip = tempTrip,
                };

                var foundCustomer = db.Customers.FirstOrDefault(Customer => Customer.Givenname == inTicket.Customer.Givenname);

                if(foundCustomer == null)
                {
                    var tempCustomer = inTicket.Customer;
                    System.Diagnostics.Debug.WriteLine(tempCustomer.Zipcode.Postalcode);
                    var tempZipcode = db.Zipcodes.FirstOrDefault(zip => zip.Postalcode == tempCustomer.Zipcode.Postalcode);

                    Customer customer = new Customer()
                    {
                        Givenname = tempCustomer.Givenname,
                        Surname = tempCustomer.Surname,
                        Id = tempCustomer.Id,
                        Address = tempCustomer.Address,
                        Zipcode = tempZipcode,
                        Tickets = new List<Ticket>()
                    };
                    customer.Tickets.Add(ticket);

                    try
                    {
                        db.Customers.Add(customer);
                        db.SaveChanges();
                        return true;
                    }
                    catch(Exception error)
                    {
                        System.Diagnostics.Debug.WriteLine(error);
                        return false;
                    }
                }
                else
                {
                    try
                    {
                        System.Diagnostics.Debug.WriteLine(foundCustomer);
                        foundCustomer.Tickets.Add(ticket);
                        db.SaveChanges();
                        return true;
                    } 
                    catch(Exception error)
                    {
                        System.Diagnostics.Debug.WriteLine(error);
                        return false;
                    }
                }
            }
        }

    }
}