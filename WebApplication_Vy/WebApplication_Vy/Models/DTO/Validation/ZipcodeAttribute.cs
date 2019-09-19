using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Db.Repositories.Implementation;

namespace WebApplication_Vy.Models.DTO.Validation
{
    public class ZipcodeAttribute : ValidationAttribute
    {
        private readonly IVyRepository _repository = new VyRepositoryImpl();

        public override bool IsValid(object value)
        {
            var zipcodes = _repository.findAllZipcodes();
            Debug.WriteLine(value);
            var zipcode = (string) value;

            foreach (var zip in zipcodes)
                if (zip.Postalcode == zipcode)
                    return true;
            return false;
        }
    }
}