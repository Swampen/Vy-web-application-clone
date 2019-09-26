using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Vy.Models.DTO;
using WebApplication_Vy.Models.DTO.TripData;

namespace WebApplication_Vy.Service.Contracts
{
    public interface IVyService
    {
        List<CustomerDTO> GetCustomerDtos();

        List<TicketDTO> GetTicketDtos();

        bool CreateTicket(TicketDTO ticketDTO);
    }
}