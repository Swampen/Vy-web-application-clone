namespace DAL.DTO
{
    public class SubmitPurchaseDto
    {
        public TicketDto TripTicket { get; set; }
        public TicketDto ReturnTripTicket { get; set; }
    }
}