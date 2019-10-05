namespace WebApplication_Vy.Models.Entities
{
    public class CreditCard
    {
        public int Id { get; set; }
        public string CardholderName { get; set; }
        public string CreditCardNumber { get; set; }
        public string Cvc { get; set; }
    }
}