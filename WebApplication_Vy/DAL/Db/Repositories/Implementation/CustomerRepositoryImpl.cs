using DAL.Db.Repositories.Contracts;
using log4net;
using MODEL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILS.Utils.Logging;

namespace DAL.Db.Repositories.Implementation
{

    public class CustomerRepositoryImpl : ICustomerRepository
    {
        private static readonly ILog Log = LogHelper.GetLogger();
        public bool updateCustomer(Customer innCustomer)
        {
            var db = new VyDbContext();
            var foundCustomer = db.Customers.Find(innCustomer.Id);

            if (foundCustomer != null)
            {
                Zipcode zip = db.Zipcodes.Find(innCustomer.Zipcode.Postalcode);
                try
                {
                    foundCustomer.Givenname = innCustomer.Givenname;
                    foundCustomer.Surname = innCustomer.Surname;
                    foundCustomer.Email = innCustomer.Email;
                    foundCustomer.Address = innCustomer.Address;
                    foundCustomer.Zipcode = zip;
                    foundCustomer.Givenname = innCustomer.Givenname;
                    db.SaveChanges();
                    Log.Info(LogEventPrefixes.DATABASE_ACCESS + "Updated information for customer with ID: " + innCustomer.Id);
                    return true;
                }
                catch (Exception error)
                {
                    Log.Error(LogEventPrefixes.DATABASE_ERROR + error.Message, error);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool deleteCustomer(int customerId)
        {
            var db = new VyDbContext();
            var foundCustomer = db.Customers.Find(customerId);

            if (foundCustomer != null)
            {
                try
                {
                    db.Customers.Remove(foundCustomer);
                    db.SaveChanges();
                    Log.Info(LogEventPrefixes.DATABASE_ACCESS + "Deleted customer with ID: " + customerId);
                    return true;
                }
                catch (Exception error)
                {
                    Log.Error(LogEventPrefixes.DATABASE_ERROR + error.Message, error);
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        public List<Customer> getAllCustomers()
        {
            {
                var db = new VyDbContext();
                return db.Customers.ToList();
            }
        }
    }
}
