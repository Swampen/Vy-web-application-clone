using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Models.Entities;

namespace WebApplication_Vy.Db.Repositories.Implementation
{
    public class VyRepositoryImpl : IVyRepository
    {
        public List<Ticket> FindAllTickets()
        {
            var db = new VyDbContext();
            return db.Tickets.ToList();
        }

        public List<Customer> FindAllCustomers()
        {
            var db = new VyDbContext();
            return db.Customers.ToList();
        }

        public List<Zipcode> FindAllZipcodes()
        {
            var db = new VyDbContext();
            return db.Zipcodes.ToList();
        }

        public Zipcode FindZipcode(string postalcode)
        {
            var db = new VyDbContext();
            var zipcode = db.Zipcodes.FirstOrDefault(zip => zip.Postalcode == postalcode);
            return zipcode;
        }
        
        //TODO: Vi gjør litt dobbelt opp med mapping når vi først går fra dto til
        //entitet, for så å mappe til en ny ticket i denne metoden. Her kan det gjøres en god del optimalisering
        public bool CreateTicket(Ticket inTicket)
        {
            Ticket ticket = new Ticket
            {
                DepartureStation = inTicket.DepartureStation,
                ArrivalStation = inTicket.ArrivalStation,
                DepartureTime = inTicket.DepartureTime,
                ArrivalTime = inTicket.ArrivalTime,
                Duration = inTicket.Duration,
                Price = inTicket.Price,
                TrainChanges = inTicket.TrainChanges,
                CreditCard = inTicket.CreditCard
            };
            
            using (var db = new VyDbContext())
            {
                var foundCustomer = db
                    .Customers
                    .FirstOrDefault(customer =>
                        customer.Email.Equals(inTicket.Customer.Email));

                if (foundCustomer == null)
                {
                    var customer = new Customer
                    {
                        Givenname = inTicket.Customer.Givenname,
                        Surname = inTicket.Customer.Surname,
                        Address = inTicket.Customer.Address,
                        Email = inTicket.Customer.Email,
                        Zipcode = db
                            .Zipcodes
                            .FirstOrDefault(zip => zip.Postalcode == inTicket.Customer.Zipcode.Postalcode),
                        Tickets = new List<Ticket>
                        {
                            ticket
                        },
                    };

                    try
                    {
                        db.Customers.Add(customer);
                        db.SaveChanges();
                        return true;
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine(error);
                        Console.WriteLine(error.StackTrace);
                        return false;
                    }
                }

                try
                {
                    inTicket.Customer = foundCustomer;
                    foundCustomer.Tickets.Add(inTicket);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception error)
                {
                    Debug.WriteLine(error);
                    return false;
                }
            }
        }

        public bool DeleteTicket(int ticketId)
        {
            var db = new VyDbContext();
            try {
                Ticket ticket = db.Tickets.Find(ticketId);
                db.Tickets.Remove(ticket);
                db.SaveChanges();
                return true;
            }
            catch (Exception e){
                System.Diagnostics.Debug.WriteLine(e);
                return false;
            }
        }
    }
}