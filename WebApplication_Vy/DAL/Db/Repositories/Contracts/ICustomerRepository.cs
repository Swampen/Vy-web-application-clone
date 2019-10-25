using MODEL.Models.Entities;
using System.Collections.Generic;

namespace DAL.Db.Repositories.Contracts
{
    public interface ICustomerRepository
    {
        List<Customer> getAllCustomers();

        bool updateCustomer(Customer innCustomer);

        bool deleteCustomer(int customerId);
    }
}
