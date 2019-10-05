using WebApplication_Vy.Models.DTO;

namespace WebApplication_Vy.Service.Contracts
{
    public interface ICreditCardService
    {
        CardDTO GetCreditCard(int id);
        CardDTO GetCreditCardFromTicketId(int ticketId);
    }
    
}