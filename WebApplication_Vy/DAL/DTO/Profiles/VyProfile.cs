using AutoMapper;
using DAL.DTO.TripData;
using MODEL.Models.Entities;
using MODEL.Models.Entities.TripData;

namespace DAL.DTO.Profiles
{
    public class CusToCusDTOProfile : Profile
    {
        public CusToCusDTOProfile()
        {
            CreateMap<Customer, CustomerDto>();
        }
    }

    public class TicToTicDTOProfile : Profile
    {
        public TicToTicDTOProfile()
        {
            CreateMap<Ticket, TicketDto>();
        }
    }

    public class TriToTriDTOProfile : Profile
    {
        public TriToTriDTOProfile()
        {
            CreateMap<Trip, TripDTO>();
        }
    }

    public class ZipToZipDTOProfile : Profile
    {
        public ZipToZipDTOProfile()
        {
            CreateMap<Zipcode, ZipcodeDto>();
        }
    }


}