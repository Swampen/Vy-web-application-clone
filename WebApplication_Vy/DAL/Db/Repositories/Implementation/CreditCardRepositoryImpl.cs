using System.Linq;
using DAL.Db.Repositories.Contracts;
using MODEL.Models.Entities;

namespace DAL.Db.Repositories.Implementation
{
    public class CreditCardRepositoryImpl : ICreditCardRepository
    {
        public CreditCard GetCardById(int cardId)
        {
            using (var db = new VyDbContext())
            {
                return Queryable.FirstOrDefault<CreditCard>(db.CreditCards, card => card.Id.Equals(cardId));
            }
        }

        public CreditCard GetCardByCardNumber(string cardNumber)
        {
            using (var db = new VyDbContext())
            {
                return Queryable.FirstOrDefault<CreditCard>(db.CreditCards, card => card.CreditCardNumber.Equals(cardNumber));
            }
        }

        public CreditCard GetCardByTicketId(int ticketId)
        {
            using (var db = new VyDbContext())
            {
                
                Ticket foundTicket = Queryable.FirstOrDefault<Ticket>(db.Tickets, ticket => ticket.Id == ticketId);
                return foundTicket?.CreditCard;
            }
        }
    }
}