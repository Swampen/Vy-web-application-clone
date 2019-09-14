using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication_Vy.Models.Entities;

namespace WebApplication_Vy.Models.DTO.Validation
{
    public class ZipcodeValidation : ValidationAttribute
    {
        private static Db.Repositories.Contracts.VyRepository Repository = new Db.Repositories.Implementation.VyRepositoryImpl();
        private List<Zipcode> zipcodes = Repository.findAllZipcodes();
        protected override ValidationResult IsValid(ValidationContext validationContext)
        {
            if (zipcodes.Contains(validationContext.DisplayName.ToString())){
                return ValidationResult.Success;
            }
            return new ValidationResult("Not a valid Norwegian zipcode");
        }
    }
}