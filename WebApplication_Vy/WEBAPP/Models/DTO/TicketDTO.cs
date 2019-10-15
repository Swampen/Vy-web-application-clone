namespace WebApplication_Vy.Models.DTO
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public int Price { get; set; }
        public string Duration { get; set; }
        public string TrainChanges { get; set; }
        public CardDto CreditCard { get; set; }
        public CustomerDto Customer { get; set; }
    }
}