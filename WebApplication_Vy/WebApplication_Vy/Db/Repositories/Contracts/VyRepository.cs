using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Vy.Db.Repositories.Contracts
{
    public interface VyRepository
    {
        List<Ticket> findAll();

        List<Trip> findAll();

        List<Customer> findAll();
    }
}