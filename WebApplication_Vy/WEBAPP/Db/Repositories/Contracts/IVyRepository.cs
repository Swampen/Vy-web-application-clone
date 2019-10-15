using System.Collections.Generic;
using WebApplication_Vy.Models.Entities;

namespace WebApplication_Vy.Db.Repositories.Contracts
{
    public interface IVyRepository
    {
        List<Ticket> FindAllTickets();

        List<Customer> FindAllCustomers();

        List<Zipcode> FindAllZipcodes();

        bool CreateTicket(Ticket inTicket);

        bool DeleteTicket(int ticketId);

        Zipcode FindZipcode(string postalcode);
    }
}