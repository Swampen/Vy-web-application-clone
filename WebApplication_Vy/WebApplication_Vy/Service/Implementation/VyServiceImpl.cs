using System.Collections.Generic;
using System.Xml;
using AutoMapper;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Models.DTO;
using WebApplication_Vy.Models.DTO.TripData;
using WebApplication_Vy.Models.Entities;
using WebApplication_Vy.Service.Contracts;

namespace WebApplication_Vy.Service.Implementation
{
    public class VyServiceImpl : IVyService
    {
        private readonly IVyRepository _vyRepository;

        public VyServiceImpl(IVyRepository vyRepository)
        {
            _vyRepository = vyRepository;
        }

        public List<CustomerDTO> GetCustomerDtos()
        {
            var entities = _vyRepository.findAllCustomers();
            var dtos = new List<CustomerDTO>();
            foreach (var entity in entities) dtos.Add(MapCustomerDto(entity));
            return dtos;
        }

        public List<TicketDTO> GetTicketDtos()
        {
            var customers = _vyRepository.findAllCustomers();
            var dtos = new List<TicketDTO>();
            foreach (var customer in customers)
            foreach (var ticket in customer.Tickets)
                dtos.Add(MapTicketDto(ticket));
            return dtos;
        }


        public bool CreateTicket(SubmitPurchaseDTO submitPurchaseDto)
        {
            var ticket = MapTicketEntity(submitPurchaseDto);
            return _vyRepository.createTicket(ticket);
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
            var dto = mapper.Map<CustomerDTO>(entity);
            return dto;
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
            var dto = mapper.Map<TicketDTO>(entity);
            return dto;
        }

        private Trip MapTripEntity(TripDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TripDTO, Trip>());
            var mapper = new Mapper(config);
            var entity = mapper.Map<Trip>(dto);
            return entity;
        }
/*
        private TripTicket MapTicketEntity(SubmitPurchaseDTO dto)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TicketDTO, TripTicket>();
                cfg.CreateMap<TripDTO, Trip>();
                cfg.CreateMap<CustomerDTO, Customer>();
                cfg.CreateMap<ZipcodeDTO, Zipcode>();
                cfg.CreateMap<SubmitPurchaseDTO, TripTicket>();
            });
            var mapper = new Mapper(config);
            var entity = mapper.Map<TripTicket>(dto);
            return entity;
        }
        */

        private Ticket MapTicketEntity(SubmitPurchaseDTO dto)
        {
            Ticket ticket = new Ticket();
            ticket.Customer = MapCustomerEntity(dto.TripTicket.Customer);
            ticket.ArrivalStation = dto.TripTicket.ArrivalStation;
            ticket.DepartureStation = dto.TripTicket.DepartureStation;
            ticket.ArrivalTime = dto.TripTicket.ArrivalTime;
            ticket.DepartureTime = dto.TripTicket.DepartureTime;
            ticket.Duration = dto.TripTicket.Duration;
            ticket.TrainChanges = dto.TripTicket.TrainChanges;
            ticket.Price = dto.TripTicket.Price;
            return ticket;
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
            var entity = mapper.Map<Customer>(dto);
            return entity;
        }
    }
}