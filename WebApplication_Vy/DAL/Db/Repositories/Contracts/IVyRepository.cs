using System.Collections.Generic;
using DAL.DTO;
using MODEL.Models.Entities;

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

        bool ChangeStation(StationDTO stationdto);
    }
}