using WebApplication_Vy.Models.Entities;

namespace WebApplication_Vy.Db.Repositories.Contracts
{
    public interface ICreditCardRepository
    {
        CreditCard GetCardById(int cardId);
        CreditCard GetCardByCardNumber(string cardNumber);

        CreditCard GetCardByTicketId(int ticketId);
    }
}