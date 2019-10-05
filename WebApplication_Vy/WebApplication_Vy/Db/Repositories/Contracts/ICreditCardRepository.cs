using System.Collections.Generic;
using WebApplication_Vy.Models.DTO;
using WebApplication_Vy.Models.Entities;

namespace WebApplication_Vy.Db.Repositories.Contracts
{
    public interface ICreditCardRepository
    {
        List<CreditCard> getCardsByCustomerId(int customerId);
        CreditCard getCardById(int cardId);
        CreditCard getCardByCardNumber(string cardNumber);

        CreditCard getCardByTicketId(int ticketId);
    }
}