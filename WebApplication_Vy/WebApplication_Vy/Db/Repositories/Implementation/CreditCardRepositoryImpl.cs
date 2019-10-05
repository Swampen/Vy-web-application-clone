using System.Collections.Generic;
using System.Linq;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Models.Entities;

namespace WebApplication_Vy.Db.Repositories.Implementation
{
    public class CreditCardRepositoryImpl : ICreditCardRepository
    {
        public List<CreditCard> getCardsByCustomerId(int customerId)
        {
            using (var db = new VyDbContext())
            {
                return db.CreditCards
                    .Where(card => card.CardHolder.Id.Equals(customerId))
                    .ToList();
            }
        }

        public CreditCard getCardById(int cardId)
        {
            using (var db = new VyDbContext())
            {
                return db.CreditCards
                    .FirstOrDefault(card => card.Id.Equals(cardId));
            }
        }

        public CreditCard getCardByCardNumber(string cardNumber)
        {
            using (var db = new VyDbContext())
            {
                return db.CreditCards
                    .FirstOrDefault(card => card.CreditCardNumber.Equals(cardNumber));
            }
        }

        public CreditCard getCardByTicketId(int ticketId)
        {
            using (var db = new VyDbContext())
            {
                return db.Tickets.Find(ticketId).CreditCard;
            }
        }
    }
}