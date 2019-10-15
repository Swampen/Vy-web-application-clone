using System.Collections.Generic;
using DAL.DTO;

namespace DAL.Service.Contracts
{
    public interface IVyService
    {
        List<CustomerDto> GetCustomerDtos();

        List<TicketDto> GetTicketDtos();

        bool CreateTicket(TicketDto ticketDto);

        bool DeleteTicket(int ticketId);

        void MaskCreditCardNumber(CardDto cardDto);
    }
}