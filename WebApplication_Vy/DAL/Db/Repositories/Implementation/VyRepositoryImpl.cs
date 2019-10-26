using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Db.Repositories.Contracts;
using log4net;
using MODEL.Models.Entities;
using UTILS.Utils.Logging;

namespace DAL.Db.Repositories.Implementation
{
    public class VyRepositoryImpl : IVyRepository
    {
        private static readonly ILog Log = LogHelper.GetLogger();

        public List<Ticket> FindAllTickets()
        {
            Log.Info(LogEventPrefixes.DATABASE_ACCESS + ": fetching all tickets from database");
            var db = new VyDbContext();
            return db.Tickets.ToList();
        }

        public List<Customer> FindAllCustomers()
        {
            Log.Info(LogEventPrefixes.DATABASE_ACCESS + ": fetching all customers from database");
            var db = new VyDbContext();
            return db.Customers.ToList();
        }

        public List<Zipcode> FindAllZipcodes()
        {
            Log.Info(LogEventPrefixes.DATABASE_ACCESS + ": fetching all zipcodes from database");
            var db = new VyDbContext();
            return db.Zipcodes.ToList();
        }

        public Zipcode FindZipcode(string postalcode)
        {
            Log.Info(LogEventPrefixes.DATABASE_ACCESS +
                     ": fetching zipcode: " + postalcode + "from database");
            var db = new VyDbContext();
            return db.Zipcodes.FirstOrDefault(zip => zip.Postalcode == postalcode);
        }

        public bool CreateTicket(Ticket inTicket)
        {
            var ticket = new Ticket
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
                    .Customers.FirstOrDefault(customer =>
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
                            .Zipcodes.FirstOrDefault(zip => zip.Postalcode == inTicket.Customer.Zipcode.Postalcode),
                        Tickets = new List<Ticket>
                        {
                            ticket
                        }
                    };

                    try
                    {
                        db.Customers.Add(customer);
                        db.SaveChanges();
                        Log.Info(LogEventPrefixes.DATABASE_ACCESS +
                                 ": Create customer event succeded for customerId: " + customer.Id);
                        return true;
                    }
                    catch (Exception error)
                    {
                        Log.Error(LogEventPrefixes.DATABASE_ERROR + ": " + error.Message, error);
                        return false;
                    }
                }

                try
                {
                    inTicket.Customer = foundCustomer;
                    foundCustomer.Tickets.Add(inTicket);
                    db.SaveChanges();
                    Log.Info(LogEventPrefixes.DATABASE_ACCESS +
                             "Create ticket event succeded for customerId: " + foundCustomer.Id);
                    return true;
                }
                catch (Exception error)
                {
                    Log.Error(LogEventPrefixes.DATABASE_ERROR + ": " + error.Message, error);
                    return false;
                }
            }
        }

        public bool DeleteTicket(int ticketId)
        {
            var db = new VyDbContext();
            try
            {
                var ticket = db.Tickets.Find(ticketId);
                if (ticket != null)
                {
                    db.Tickets.Remove(ticket);
                    db.SaveChanges();
                    Log.Info(LogEventPrefixes.DATABASE_ACCESS +
                             ": Deleted ticketId: " + ticket.Id);
                    return true;
                }
            }
            catch (Exception e)
            {
                Log.Error(LogEventPrefixes.DATABASE_ERROR + ": ticket delete failed" + e.Message, e);
                return false;
            }

            Log.Warn(LogEventPrefixes.DATABASE_ACCESS + ": delete failed, could not find ticketId: " + ticketId);
            return false;
        }
    }
}