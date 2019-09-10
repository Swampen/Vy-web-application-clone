using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Vy.Models.Entities;

namespace WebApplication_Vy.Db.Repositories.Implementation
{
    public class VyRepositoryImpl : WebApplication_Vy.Db.Repositories.Contracts.VyRepository
    {
        public List<Ticket> findAllTickets()
        {
            var db = new VyDbContext();
            return db.Tickets.ToList();
        }

        public List<Trip> findAllTrips()
        {
            var db = new VyDbContext();
            return db.Trips.ToList();
        }

        public List<Customer> findAllCustomers()
        {
            var db = new VyDbContext();
            return db.Customers.ToList();
        }

        public bool newTicket(Ticket inTicket)
        {
            using (var db = new VyDbContext())
            {
                var ticket = new Ticket()
                {
                    Departure = inTicket.Departure,
                    Roundtrip = inTicket.Roundtrip,
                    Trip = inTicket.Trip,
                };

                var foundCustomer = db.Customers.FirstOrDefault(Customer => Customer.Givenname == inTicket.Customer.Givenname);

                if(foundCustomer == null)
                {
                    var temp = inTicket.Customer;
                    Customer customer = new Customer()
                    {
                        Givenname = temp.Givenname,
                        Surname = temp.Surname,
                        Id = temp.Id,
                        Address = temp.Address,
                        Zipcode = temp.Zipcode,
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