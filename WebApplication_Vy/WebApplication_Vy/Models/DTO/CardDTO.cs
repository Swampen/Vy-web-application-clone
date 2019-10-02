using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication_Vy.Models.DTO
{
    public class CardDTO
    {
        [RegularExpression(@"([0-9]{4} ){4}", ErrorMessage = "Not a valid credit card")]
        [Display(Name = "Credit card number")]
        public string Card_Number { get; set; }

        public string Expiry { get; set; }

        [RegularExpression(@"[0-9]{3}", ErrorMessage = "Not a valid CVC")]
        [Display(Name = "CVC")]
        public int Cvc { get; set; }

        [Display(Name = "Cardholder name")]
        public string Card_Holder { get; set; }
    }
}