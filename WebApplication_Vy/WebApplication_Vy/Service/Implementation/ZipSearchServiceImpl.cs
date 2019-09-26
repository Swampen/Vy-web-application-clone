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
            _vyRepository = _vyRepository;
        }
        
        public string GetPostaltown(string postalcode)
        {
            Zipcode zipcode = _vyRepository.findZipcode(postalcode);
            if (zipcode == null)
            {
                return "";
            }
            return MapZipcodeDTO(zipcode).Postaltown;
        }
        
        private ZipcodeDTO MapZipcodeDTO(Zipcode entity)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Zipcode, ZipcodeDTO>().ReverseMap());
            var mapper = config.CreateMapper();
            ZipcodeDTO dto = mapper.Map<ZipcodeDTO>(entity);
            return dto;
        }
        

    }
}