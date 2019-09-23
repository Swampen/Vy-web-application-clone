using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Vy.Models.DTO;
using WebApplication_Vy.Models.DTO.TripData;
using WebApplication_Vy.Models.Entities;
using WebApplication_Vy.Db.Repositories.Implementation;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Service.Contracts;
using AutoMapper;

namespace WebApplication_Vy.Service.Implementation
{
    public class VyServiceImpl : IVyService
    {

        private IVyRepository _vyRepository;

        public VyServiceImpl(IVyRepository vyRepository)
        {
            _vyRepository = vyRepository;
        }
        public List<CustomerDTO> GetCustomerDtos()
        {
            List<Customer> entities = _vyRepository.findAllCustomers();
            List<CustomerDTO> dtos = new List<CustomerDTO>();
            foreach (Customer entity in entities)
            {
                dtos.Add(MapCustomerDto(entity));
            }
            return dtos;
        }

        private CustomerDTO MapCustomerDto(Customer entity)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Ticket, TicketDTO>().ReverseMap();
                cfg.CreateMap<Customer, CustomerDTO>().ReverseMap();
                cfg.CreateMap<Trip, TripDTO>().ReverseMap();
                cfg.CreateMap<Zipcode, ZipcodeDTO>().ReverseMap();
            });
            var mapper = config.CreateMapper();
            CustomerDTO dto = mapper.Map<CustomerDTO>(entity);
            return dto;
        }

        private ZipcodeDTO MapZipcodeDTO(Zipcode entity)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Zipcode, ZipcodeDTO>().ReverseMap());
            var mapper = config.CreateMapper();
            ZipcodeDTO dto = mapper.Map<ZipcodeDTO>(entity);
            return dto;
        }

        public List<TicketDTO> GetTicketDtos()
        {
            List<Customer> customers = _vyRepository.findAllCustomers();
            List<TicketDTO> dtos = new List<TicketDTO>();
            foreach (Customer customer in customers)
            {
                foreach (Ticket ticket in customer.Tickets)
                    dtos.Add(MapTicketDto(ticket));
            }
            return dtos;
        }

        private TicketDTO MapTicketDto(Ticket entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Ticket, TicketDTO>().ReverseMap();
                cfg.CreateMap<Customer, CustomerDTO>().ReverseMap();
                cfg.CreateMap<Trip, TripDTO>().ReverseMap();
                cfg.CreateMap<Zipcode, ZipcodeDTO>().ReverseMap();
            });
            var mapper = config.CreateMapper();
            TicketDTO dto = mapper.Map<TicketDTO>(entity);
            return dto;
        }

        
        public bool CreateTicket(TicketDTO ticketDTO)
        {
            Ticket ticket = MapTicketEntity(ticketDTO);
            return _vyRepository.createTicket(ticket);
        }

        private Trip MapTripEntity(TripDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TripDTO, Trip>());
            var mapper = new Mapper(config);
            Trip entity = mapper.Map<Trip>(dto);
            return entity;
        }

        private Ticket MapTicketEntity(TicketDTO dto)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TicketDTO, Ticket>();
                cfg.CreateMap<TripDTO, Trip>();
                cfg.CreateMap<CustomerDTO, Customer>();
                cfg.CreateMap<ZipcodeDTO, Zipcode>();
            });
            var mapper = new Mapper(config);
            Ticket entity = mapper.Map<Ticket>(dto);
            return entity;
        }

        private Customer MapCustomerEntity(CustomerDTO dto)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TicketDTO, Ticket>();
                cfg.CreateMap<TripDTO, Trip>();
                cfg.CreateMap<CustomerDTO, Customer>();
                cfg.CreateMap<ZipcodeDTO, Zipcode>();
            });
            var mapper = new Mapper(config);
            Customer entity = mapper.Map<Customer>(dto);
            return entity;
        }

        private Zipcode MapZipcodeEntity(ZipcodeDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ZipcodeDTO, Zipcode>());
            var mapper = new Mapper(config);
            Zipcode entity = mapper.Map<Zipcode>(dto);
            return entity;
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
    }
}