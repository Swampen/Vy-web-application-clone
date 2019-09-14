using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Vy.Models.DTO;

namespace WebApplication_Vy.Service.Contracts
{
    public interface IVyService
    {
        List<CustomerDTO> GetCustomerDtos();

        List<TicketDTO> GetTicketDtos();

        List<TripDTO> GetTripDtos();

        bool CreateTicket(TicketDTO ticketDTO);

        string GetPostaltown(string postalcode);
    }
}