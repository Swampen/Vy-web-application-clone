﻿using MODEL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Db.Repositories.Contracts
{
    public interface ICustomerRepository
    {
        List<Customer> getAllCustomers();

        bool updateCustomer(Customer innCustomer);

        bool deleteCustomer(int customerId);
    }
}