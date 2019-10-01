using System.Collections.Generic;

namespace WebApplication_Vy.Models.DTO
{
    public class SubmitPurchaseDTO
    {
        public TicketDTO Ticket { get; set; }
        public List<TicketDTO> Tickets { get; set; }
    }
}