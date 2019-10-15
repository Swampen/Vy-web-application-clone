using MODEL.Models.Entities;

namespace DAL.Db.Repositories.Contracts
{
    public interface ICreditCardRepository
    {
        CreditCard GetCardById(int cardId);
        CreditCard GetCardByCardNumber(string cardNumber);

        CreditCard GetCardByTicketId(int ticketId);
    }
}