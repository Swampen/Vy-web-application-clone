using DAL.Db.Repositories.Contracts;
using MODEL.Models.Entities;
using System.Linq;
using log4net;
using UTILS.Utils.Logging;

namespace DAL.Db.Repositories.Implementation
{
    public class CreditCardRepositoryImpl : ICreditCardRepository
    {

        public static readonly ILog Log = LogHelper.GetLogger();
        public CreditCard GetCardById(int cardId)
        {
            using (var db = new VyDbContext())
            {
                Log.Info(LogEventPrefixes.DATABASE_ACCESS + ": fethced creditcard with id: " + cardId);
                return Queryable.FirstOrDefault<CreditCard>(db.CreditCards, card => card.Id.Equals(cardId));
            }
        }

        public CreditCard GetCardByCardNumber(string cardNumber)
        {
            using (var db = new VyDbContext())
            {
                Log.Info(LogEventPrefixes.DATABASE_ACCESS + ": fetch creditcard by cardnumber");
                return Queryable.FirstOrDefault<CreditCard>(db.CreditCards, card => card.CreditCardNumber.Equals(cardNumber));
            }
        }

        public CreditCard GetCardByTicketId(int ticketId)
        {
            using (var db = new VyDbContext())
            {
                Log.Info(LogEventPrefixes.DATABASE_ACCESS + ": fetch creditcard by ticketId: " + ticketId);
                Ticket foundTicket = Queryable.FirstOrDefault<Ticket>(db.Tickets, ticket => ticket.Id == ticketId);
                return foundTicket?.CreditCard;
            }
        }
    }
}