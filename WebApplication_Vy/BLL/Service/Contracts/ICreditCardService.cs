using DAL.DTO;

namespace BLL.Service.Contracts
{
    public interface ICreditCardService
    {
        CardDto GetCreditCard(int id);
        CardDto GetCreditCardFromTicketId(int ticketId);
    }

}