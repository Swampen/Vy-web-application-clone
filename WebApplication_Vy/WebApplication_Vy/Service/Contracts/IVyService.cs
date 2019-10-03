using System.Collections.Generic;
using WebApplication_Vy.Models.DTO;

namespace WebApplication_Vy.Service.Contracts
{
    public interface IVyService
    {
        List<CustomerDTO> GetCustomerDtos();

        List<TicketDTO> GetTicketDtos();

        bool CreateTicket(TicketDTO ticketDto);
    }
}