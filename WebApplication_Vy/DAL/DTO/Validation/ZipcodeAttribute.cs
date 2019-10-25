using DAL.Db.Repositories.Contracts;
using DAL.Db.Repositories.Implementation;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace DAL.DTO.Validation
{
    public class ZipcodeAttribute : ValidationAttribute
    {
        private readonly IVyRepository _repository = new VyRepositoryImpl();

        public override bool IsValid(object value)
        {
            var zipcodes = _repository.FindAllZipcodes();
            Debug.WriteLine(value);
            var zipcode = (string)value;

            foreach (var zip in zipcodes)
                if (zip.Postalcode == zipcode)
                    return true;
            return false;
        }
    }
}