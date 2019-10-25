using MODEL.Models.Entities;
using System.Collections.Generic;

namespace DAL.Db.Repositories.Contracts
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