using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WebApplication_Vy.Models.Entities;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Db.Repositories.Implementation;


namespace WebApplication_Vy.Models.DTO.Validation
{
    public class ZipcodeAttribute : ValidationAttribute
    {
        private static VyRepository Repository = new VyRepositoryImpl();
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