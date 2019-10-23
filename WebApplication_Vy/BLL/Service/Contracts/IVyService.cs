using System.Collections.Generic;
using DAL.DTO;

namespace BLL.Service.Contracts
{
    public interface IVyService
    {
        List<CustomerDto> GetCustomerDtos();

        List<TicketDto> GetTicketDtos();

        bool UpdateCustomer(CustomerDto customer);

        bool DeleteCustomer(int id);

        bool CreateTicket(TicketDto ticketDto);

        bool DeleteTicket(int ticketId);

        void MaskCreditCardNumber(CardDto cardDto);

        bool ChangeStation(StationDTO station);
    }
}