using System.Collections.Generic;
using System.Linq;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Models.Entities;

namespace WebApplication_Vy.Db.Repositories.Implementation
{
    public class CreditCardRepositoryImpl : ICreditCardRepository
    {

        public CreditCard GetCardById(int cardId)
        {
            using (var db = new VyDbContext())
            {
                return db.CreditCards
                    .FirstOrDefault(card => card.Id.Equals(cardId));
            }
        }

        public CreditCard GetCardByCardNumber(string cardNumber)
        {
            using (var db = new VyDbContext())
            {
                return db.CreditCards
                    .FirstOrDefault(card => card.CreditCardNumber.Equals(cardNumber));
            }
        }

        public CreditCard GetCardByTicketId(int ticketId)
        {
            using (var db = new VyDbContext())
            {
                return db.Tickets.FirstOrDefault(ticket => ticket.Id == ticketId).CreditCard;
            }
        }
    }
}