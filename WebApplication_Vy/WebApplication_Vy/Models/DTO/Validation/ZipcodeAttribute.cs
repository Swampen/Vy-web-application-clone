using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication_Vy.Models.Entities;

namespace WebApplication_Vy.Models.DTO.Validation
{
    public class ZipcodeAttribute : ValidationAttribute
    {
        private static Db.Repositories.Contracts.VyRepository Repository = new Db.Repositories.Implementation.VyRepositoryImpl();
        private List<Zipcode> zipcodes = Repository.findAllZipcodes();

        public override bool IsValid(object value)
        {
            System.Diagnostics.Debug.WriteLine(value);
            string zipcode = (string)value;

            foreach (var zip in zipcodes)
            {
                if (zip.Postalcode == zipcode)
                {
                    return true;
                }
            }
            return false;

        }
    }
}