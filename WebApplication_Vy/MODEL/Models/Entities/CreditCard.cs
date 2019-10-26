using System.ComponentModel.DataAnnotations;

namespace MODEL.Models.Entities
{
    [TrackChanges]
    public class CreditCard
    {
        public int Id { get; set; }
        public string CardholderName { get; set; }
        public string CreditCardNumber { get; set; }
        public string Cvc { get; set; }
    }
}