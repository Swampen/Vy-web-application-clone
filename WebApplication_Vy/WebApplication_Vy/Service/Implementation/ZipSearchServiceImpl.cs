using System.Text.RegularExpressions;
using AutoMapper;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Models.DTO;
using WebApplication_Vy.Models.Entities;
using WebApplication_Vy.Service.Contracts;

namespace WebApplication_Vy.Service.Implementation
{
    public class ZipSearchServiceImpl : IZipSearchService
    {
        private readonly IVyRepository _vyRepository;

        public ZipSearchServiceImpl(IVyRepository vyRepository)
        {
            _vyRepository = vyRepository;
        }

        public string GetPostaltown(string postalcode)
        {
            var match = Regex.Match(postalcode, "[0-9]{4}");
            if (!match.Success) return "";

            var zipcode = _vyRepository.FindZipcode(postalcode);
            if (zipcode == null) return "";
            return MapZipcodeDTO(zipcode).Postaltown;
        }

        private ZipcodeDto MapZipcodeDTO(Zipcode entity)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Zipcode, ZipcodeDto>().ReverseMap());
            var mapper = config.CreateMapper();
            var dto = mapper.Map<ZipcodeDto>(entity);
            return dto;
        }
    }
}