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
        public List<Ticket> findAllTickets()
        {
            var db = new VyDbContext();
            return db.Tickets.ToList();
        }

        public List<Customer> findAllCustomers()
        {
            var db = new VyDbContext();
            return db.Customers.ToList();
        }

        public List<Zipcode> findAllZipcodes()
        {
            var db = new VyDbContext();
            return db.Zipcodes.ToList();
        }

        public Zipcode findZipcode(string postalcode)
        {
            var db = new VyDbContext();
            var zipcode = db.Zipcodes.FirstOrDefault(zip => zip.Postalcode == postalcode);
            return zipcode;
        }

        public bool createTicket(Ticket inTicket)
        {
            using (var db = new VyDbContext())
            {
                var ticket = new Ticket();

                var foundCustomer = db
                    .Customers
                    .FirstOrDefault(customer => customer.Givenname == inTicket.Customer.Givenname);

                if (foundCustomer == null)
                {
                    var tempCustomer = inTicket.Customer;
                    Debug.WriteLine(tempCustomer.Zipcode.Postalcode);
                    var tempZipcode = db
                        .Zipcodes
                        .FirstOrDefault(zip => zip.Postalcode == tempCustomer.Zipcode.Postalcode);

                    var customer = new Customer
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
                    catch (Exception error)
                    {
                        Debug.WriteLine(error);
                        return false;
                    }
                }

                try
                {
                    Debug.WriteLine(foundCustomer);
                    foundCustomer.Tickets.Add(ticket);
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
    }
}