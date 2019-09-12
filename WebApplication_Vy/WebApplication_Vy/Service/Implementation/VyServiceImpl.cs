using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Vy.Models.DTO;
using WebApplication_Vy.Models.Entities;
using WebApplication_Vy.Db.Repositories.Implementation;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Service.Contracts;
using AutoMapper;

namespace WebApplication_Vy.Service.Implementation
{
    public class VyServiceImpl : Contracts.IVyService
    {
        public List<CustomerDTO> GetCustomerDtos()
        {
            VyRepository repository = new VyRepositoryImpl();
            List<Customer> entities = repository.findAllCustomers();
            List<CustomerDTO> dtos = new List<CustomerDTO>();
            foreach (Customer entity in entities)
            {
                dtos.Add(MapCustomerDto(entity));
            }
            return dtos;
        }

        private CustomerDTO MapCustomerDto(Customer entity)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Customer, CustomerDTO>());
            var mapper = config.CreateMapper();
            CustomerDTO dto = mapper.Map<CustomerDTO>(entity);
            return dto;
            
            /*CustomerDTO dto = new CustomerDTO();
            dto.Id = entity.Id;
            dto.Givenname = entity.Givenname;
            dto.Surname = entity.Surname;
            dto.Address = entity.Address;
            dto.ZipcodeDTO = MapZipcodeDTO(entity.Zipcode);
            return dto;*/
        }

        private ZipcodeDTO MapZipcodeDTO(Zipcode entity)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Zipcode, ZipcodeDTO>());
            var mapper = config.CreateMapper();
            ZipcodeDTO dto = mapper.Map<ZipcodeDTO>(entity);
            return dto;
            /*ZipcodeDTO dto = new ZipcodeDTO();
            dto.Postalcode = entity.Postalcode;
            dto.Postaltown = entity.Postaltown;
            return dto;*/
        }

        public List<TicketDTO> GetTicketDtos()
        {
            VyRepository repository = new VyRepositoryImpl();
            List<Customer> customers = repository.findAllCustomers();
            List<TicketDTO> dtos = new List<TicketDTO>();
            foreach (Customer customer in customers)
            {
                foreach(Ticket ticket in customer.Tickets)
                dtos.Add(MapTicketDto(ticket));
            }
            return dtos;
        }

        private TicketDTO MapTicketDto(Ticket entity)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Ticket, TicketDTO>());
            var mapper = config.CreateMapper();
            TicketDTO dto = mapper.Map<TicketDTO>(entity);
            dto.CustomerDTO = MapCustomerDto(entity.Customer);
            dto.CustomerDTO.ZipcodeDTO = MapZipcodeDTO(entity.Customer.Zipcode);
            dto.TripDTO = MapTripDto(entity.Trip);
            return dto;
           
            /*TicketDTO dto = new TicketDTO();
            dto.TicketNumber = entity.TicketNumber;
            dto.Roundtrip = entity.Roundtrip;
            dto.Departure = entity.Departure;
            dto.TripDTO = MapTripDto(entity.Trip);
            dto.CustomerDTO = MapCustomerDto(entity.Customer);
            return dto;*/
        }

        public List<TripDTO> GetTripDtos()
        {
            VyRepository repository = new VyRepositoryImpl();
            List<Trip> entities = repository.findAllTrips();
            List<TripDTO> dtos = new List<TripDTO>();
            foreach (Trip entity in entities)
            {
                dtos.Add(MapTripDto(entity));
            }

            return dtos;
        }

        private TripDTO MapTripDto(Trip entity)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Trip, TripDTO>());
            var mapper = config.CreateMapper();
            TripDTO dto = mapper.Map<TripDTO>(entity);
            return dto;
            /*TripDTO dto = new TripDTO();
            dto.TripId = entity.TripId;
            dto.Route = entity.Route;
            dto.Price = entity.Price;
            return dto;*/
        }

        public bool CreateTicket(TicketDTO ticketDTO)
        {
            VyRepository repository = new VyRepositoryImpl();
            Ticket ticket = MapTicketEntity(ticketDTO);
            ticket.Customer = MapCustomerEntity(ticketDTO.CustomerDTO);
            ticket.Customer.Zipcode = MapZipcodeEntity(ticketDTO.CustomerDTO.ZipcodeDTO);
            ticket.Trip = MapTripEntity(ticketDTO.TripDTO);
            return repository.createTicket(ticket);
        }

        private Trip MapTripEntity(TripDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TripDTO, Trip>());
            var mapper = new Mapper(config);
            Trip entity = mapper.Map<Trip>(dto);
            return entity;
            /*Trip entity = new Trip();
            entity.TripId = dto.TripId;
            entity.Route = dto.Route;
            entity.Price = dto.Price;
            return entity;*/
        }

        private Ticket MapTicketEntity(TicketDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TicketDTO, Ticket>());
            var mapper = new Mapper(config);
            Ticket entity = mapper.Map<Ticket>(dto);
            entity.Customer = MapCustomerEntity(dto.CustomerDTO);
            entity.Customer.Zipcode = MapZipcodeEntity(dto.CustomerDTO.ZipcodeDTO);
            entity.Trip = MapTripEntity(dto.TripDTO);
            return entity;

            /*Ticket entity = new Ticket();
            entity.TicketNumber = dto.TicketNumber;
            entity.Roundtrip = dto.Roundtrip;
            entity.Departure = dto.Departure;
            entity.Customer = MapCustomerEntity(dto.CustomerDTO);
            entity.Trip = MapTripEntity(dto.TripDTO);
            return entity;*/
        }

        private Customer MapCustomerEntity(CustomerDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CustomerDTO, Customer>());
            var mapper = new Mapper(config);
            Customer customer = mapper.Map<Customer>(dto);
            return customer;
           /* Customer entity = new Customer();
            entity.Id = dto.Id;
            entity.Givenname = dto.Givenname;
            entity.Surname = dto.Surname;
            entity.Address = dto.Address;
            entity.Zipcode = MapZipcodeEntity(dto.ZipcodeDTO);
            return entity;*/
        }

        private Zipcode MapZipcodeEntity(ZipcodeDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ZipcodeDTO, Zipcode>());
            var mapper = new Mapper(config);
            Zipcode entity = mapper.Map<Zipcode>(dto);
            return entity;
            /*Zipcode entity = new Zipcode();
            entity.Postalcode = dto.Postalcode;
            entity.Postaltown = dto.Postaltown;
            return entity;*/
        }
    }
}