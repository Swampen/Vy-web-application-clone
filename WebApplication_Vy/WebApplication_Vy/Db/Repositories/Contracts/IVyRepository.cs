﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Vy.Models.Entities;

namespace WebApplication_Vy.Db.Repositories.Contracts
{
    public interface IVyRepository
    {
        List<Ticket> findAllTickets();

        List<Trip> findAllTrips();

        List<Customer> findAllCustomers();

        bool createTicket(Ticket inTicket);
    }
}