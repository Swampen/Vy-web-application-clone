using System.Text.RegularExpressions;
using AutoMapper;
using DAL.Db.Repositories.Contracts;
using DAL.DTO;
using DAL.Service.Contracts;
using MODEL.Models.Entities;

namespace DAL.Service.Implementation
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