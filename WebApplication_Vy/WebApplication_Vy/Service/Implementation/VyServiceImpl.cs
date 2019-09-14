using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Vy.Models.DTO;
using WebApplication_Vy.Models.Entities;
using WebApplication_Vy.Db.Repositories.Implementation;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Service.Contracts;

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
            CustomerDTO dto = new CustomerDTO();
            dto.Id = entity.Id;
            dto.Givenname = entity.Givenname;
            dto.Surname = entity.Surname;
            dto.Address = entity.Address;
            dto.ZipcodeDTO = MapZipcodeDTO(entity.Zipcode);
            return dto;
        }

        private ZipcodeDTO MapZipcodeDTO(Zipcode entity)
        {
            ZipcodeDTO dto = new ZipcodeDTO();
            dto.Postalcode = entity.Postalcode;
            dto.Postaltown = entity.Postaltown;
            return dto;
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
            TicketDTO dto = new TicketDTO();
            dto.TicketNumber = entity.TicketNumber;
            dto.Roundtrip = entity.Roundtrip;
            dto.Departure = entity.Departure;
            dto.TripDTO = MapTripDto(entity.Trip);
            dto.CustomerDTO = MapCustomerDto(entity.Customer);
            return dto;
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
            TripDTO dto = new TripDTO();
            dto.TripId = entity.TripId;
            dto.Route = entity.Route;
            dto.Price = entity.Price;
            return dto;
        }

        public bool CreateTicket(TicketDTO ticketDTO)
        {
            VyRepository repository = new VyRepositoryImpl();
            Ticket ticket = MapTicketEntity(ticketDTO);
            return repository.createTicket(ticket);
        }

        private Trip MapTripEntity(TripDTO dto)
        {
            Trip entity = new Trip();
            entity.TripId = dto.TripId;
            entity.Route = dto.Route;
            entity.Price = dto.Price;
            return entity;
        }

        private Ticket MapTicketEntity(TicketDTO dto)
        {
            Ticket entity = new Ticket();
            entity.TicketNumber = dto.TicketNumber;
            entity.Roundtrip = dto.Roundtrip;
            entity.Departure = dto.Departure;
            entity.Customer = MapCustomerEntity(dto.CustomerDTO);
            entity.Trip = MapTripEntity(dto.TripDTO);
            return entity;
        }

        private Customer MapCustomerEntity(CustomerDTO dto)
        {
            Customer entity = new Customer();
            entity.Id = dto.Id;
            entity.Givenname = dto.Givenname;
            entity.Surname = dto.Surname;
            entity.Address = dto.Address;
            entity.Zipcode = MapZipcodeEntity(dto.ZipcodeDTO);
            return entity;
        }

        private Zipcode MapZipcodeEntity(ZipcodeDTO dto)
        {
            Zipcode entity = new Zipcode();
            entity.Postalcode = dto.Postalcode;
            entity.Postaltown = dto.Postaltown;
            return entity;
        }

        public string GetPostaltown(string postalcode)
        {
            VyRepository repository = new VyRepositoryImpl();
            Zipcode zipcode = repository.findZipcode(postalcode);
            if (zipcode == null)
            {
                return "Not a valid postalcode";
            }
            return MapZipcodeDTO(zipcode).Postaltown;
        }
    }
}