using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Vy.Models.DTO;
using WebApplication_Vy.Models.Entities;
using WebApplication_Vy.Db.Repositories.Implementation;
using WebApplication_Vy.Db.Repositories.Contracts;

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

        private TicketDTO MapTicketDto(Ticket ticket)
        {
            TicketDTO dto = new TicketDTO();
            dto.TicketNumber = ticket.TicketNumber;
            dto.Roundtrip = ticket.Roundtrip;
            dto.Departure = ticket.Departure;
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
    }
}