using DAL.DTO;

namespace DAL.Service.Contracts
{
    public interface ICreditCardService
    {
        CardDto GetCreditCard(int id);
        CardDto GetCreditCardFromTicketId(int ticketId);
    }
    
}