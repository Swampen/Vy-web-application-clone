using WebApplication_Vy.Models.DTO;

namespace WebApplication_Vy.Service.Contracts
{
    public interface ICreditCardService
    {
        CardDto GetCreditCard(int id);
        CardDto GetCreditCardFromTicketId(int ticketId);
    }
    
}