using System.ComponentModel.DataAnnotations;
using WebApplication_Vy.Models.DTO.Validation;

namespace WebApplication_Vy.Models.DTO
{
    public class CardDTO
    {
        [Required]
        [RegularExpression(@"[0-9]{4} [0-9]{4} [0-9]{4} [0-9]{4}", ErrorMessage = "Not a valid credit card")]
        [Display(Name = "Credit card number")]
        public string Card_Number { get; set; }

        [Required]
        [ExpiryDate(ErrorMessage = "The card has expired")]
        [Display(Name = "Expiry date")]
        public string Expiry_Date { get; set; }

        [Required]
        [RegularExpression(@"[0-9]{3}", ErrorMessage = "Not a valid CVC")]
        [Display(Name = "CVC")]
        public int Cvc { get; set; }

        [Required]
        [Display(Name = "Cardholder name")]
        public string Card_Holder { get; set; }
    }
}