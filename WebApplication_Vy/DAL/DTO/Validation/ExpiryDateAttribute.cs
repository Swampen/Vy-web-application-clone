using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DAL.DTO.Validation
{
    public class ExpiryDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var now = DateTime.UtcNow;
            string input = (string)value;

            char[] separator = { '/' };
            string[] splitted = input.Split(separator, 2);

            var month = Int32.Parse(splitted.ElementAt(0));
            var year = Int32.Parse("20" + splitted.ElementAt(1));


            if (year == now.Year && month > now.Month)
            {
                return true;
            }
            else if (year > now.Year)
            {
                return true;
            }

            return false;
        }
    }
}