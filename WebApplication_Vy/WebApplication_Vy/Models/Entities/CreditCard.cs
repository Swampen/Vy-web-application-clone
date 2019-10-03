namespace WebApplication_Vy.Models.Entities
{
    public class CreditCard
    {
        public string id { get; set; }
        public string cardholderName { get; set; }
        public string creditCardNo { get; set; }
        public string cvc { get; set; }
        public Customer cardHolder { get; set; }
    }
}