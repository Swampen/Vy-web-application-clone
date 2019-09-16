using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using WebApplication_Vy.Models.Entities;

namespace WebApplication_Vy.Models.DTO.Validation
{
    public class ZipcodeValidation : ValidationAttribute
    {
        private static Db.Repositories.Contracts.VyRepository Repository = new Db.Repositories.Implementation.VyRepositoryImpl();
        private List<Zipcode> zipcodes = Repository.findAllZipcodes();

        public override bool IsValid(object value)
        {
            System.Diagnostics.Debug.WriteLine(value);
            Zipcode zipcode = new Zipcode
            {
                Postalcode = (string)value,
            };

            bool found = zipcodes.Contains(zipcode);
            if (found)
            {
                return true;
            }
            return false;
        }
    }
}