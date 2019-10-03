using System.Collections.Generic;

namespace WebApplication_Vy.Models.DTO
{
    public class SubmitPurchaseDTO
    {
        public TicketDTO TripTicket { get; set; }
        public TicketDTO ReturnTripTicket { get; set; }
        
    }
}