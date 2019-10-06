using System;
using System.Collections.Generic;
using System.Xml;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using NUnit.Framework;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Models.DTO;
using WebApplication_Vy.Models.DTO.TripData;
using WebApplication_Vy.Models.Entities;
using WebApplication_Vy.Service.Contracts;
using WebGrease.Css.Extensions;

namespace WebApplication_Vy.Service.Implementation
{
    public class VyServiceImpl : IVyService
    {
        private readonly IVyRepository _vyRepository;

        public VyServiceImpl(IVyRepository vyRepository)
        {
            _vyRepository = vyRepository;
        }

        public void MaskCreditCardNumber(CardDto cardDto)
        {
            cardDto.Card_Number = "**** **** **** " + cardDto.Card_Number.Remove(0, 15);
        }
        
        public List<CustomerDto> GetCustomerDtos()
        {
            var entities = _vyRepository.FindAllCustomers();
            var dtos = new List<CustomerDto>();
            foreach (var entity in entities) dtos.Add(mapCustomerDto(entity));
            return dtos;
        }

        public List<TicketDto> GetTicketDtos()
        {
            var customers = _vyRepository.FindAllCustomers();
            var dtos = new List<TicketDto>();
            foreach (var customer in customers)
            foreach (var ticket in customer.Tickets)
                dtos.Add(mapTicketDto(ticket));
            return dtos;
        }


        public bool CreateTicket(TicketDto ticketDto)
        {
            var ticket = MapTicketEntity(ticketDto);
            return _vyRepository.CreateTicket(ticket);
        }

        public bool DeleteTicket(int ticketId)
        {
            return _vyRepository.DeleteTicket(ticketId);
        }
        /*
                private CustomerDTO MapCustomerDto(Customer entity)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<Ticket, TicketDTO>().ReverseMap();
                        cfg.CreateMap<Customer, CustomerDTO>().ReverseMap();
                        cfg.CreateMap<Trip, TripDTO>().ReverseMap();
                        cfg.CreateMap<Zipcode, ZipcodeDTO>().ReverseMap();
                        cfg.CreateMap<CreditCard, CardDTO>().ReverseMap();
                    });
                    var mapper = config.CreateMapper();
                    var dto = mapper.Map<CustomerDTO>(entity);
                    return dto;
                }
        */

        private CustomerDto mapCustomerDto(Customer customer)
        {
            var ticketDtos = new List<TicketDto>();
            customer.Tickets.ForEach(ticket => ticketDtos.Add(mapTicketDto(ticket)));
            
            return new CustomerDto
            {
                Id = customer.Id,
                Address = customer.Address,
                Zipcode = mapZipcodeDto(customer.Zipcode),
                Email = customer.Email,
                Givenname = customer.Givenname,
                Surname = customer.Surname,
                Tickets = ticketDtos,
            };
        }

        private ZipcodeDto mapZipcodeDto(Zipcode zipcode)
        {
            return new ZipcodeDto
            {
                Postalcode = zipcode.Postalcode,
                Postaltown = zipcode.Postaltown
            };
        }

        private TicketDto mapTicketDto(Ticket ticket)
        {
            return new TicketDto
            {
                ArrivalStation = ticket.ArrivalStation,
                ArrivalTime = ticket.ArrivalTime,
                CreditCard = mapCardDto(ticket.CreditCard),
                DepartureStation = ticket.DepartureStation,
                DepartureTime = ticket.DepartureTime,
                Duration = ticket.Duration,
                Id = ticket.Id,
                Price = ticket.Price,
                TrainChanges = ticket.TrainChanges,
            };
        }

        private CardDto mapCardDto(CreditCard card)
        {
            return new CardDto
            {
                Card_Number = card.CreditCardNumber,
                Card_Holder = card.CardholderName,
                Cvc = card.Cvc,
            };
        }



        private Trip MapTripEntity(TripDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TripDTO, Trip>());
            var mapper = new Mapper(config);
            var entity = mapper.Map<Trip>(dto);
            return entity;
        }



        private Ticket MapTicketEntity(TicketDto dto)
        {
            Ticket ticket = new Ticket();
            ticket.Customer = MapCustomerEntity(dto.Customer);
            ticket.ArrivalStation = dto.ArrivalStation;
            ticket.DepartureStation = dto.DepartureStation;
            ticket.ArrivalTime = dto.ArrivalTime;
            ticket.DepartureTime = dto.DepartureTime;
            ticket.Duration = dto.Duration;
            ticket.TrainChanges = dto.TrainChanges;
            ticket.Price = dto.Price;
            ticket.CreditCard = mapCreditCardEntity(dto.CreditCard);
            return ticket;
        }
        
        private Customer MapCustomerEntity(CustomerDto dto)
        {
            Customer customer = new Customer
            {
                Givenname = dto.Givenname,
                Surname = dto.Surname,
                Address = dto.Address,
                Zipcode = mapZipCodeEntity(dto.Zipcode),
                Email = dto.Email,
            };
            return customer;
        }

        private CreditCard mapCreditCardEntity(CardDto cardDto)
        {
                var cc = new CreditCard();
                cc.Cvc = cardDto.Cvc;
                cc.CardholderName = cardDto.Card_Holder;
                cc.CreditCardNumber = cardDto.Card_Number;
                return cc;
        }

        private Zipcode mapZipCodeEntity(ZipcodeDto dto)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ZipcodeDto, Zipcode>(); 
                
            });
            var mapper = new Mapper(config);
            return mapper.Map<Zipcode>(dto);
        }

        /*
               private Customer MapCustomerEntity(CustomerDTO dto)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<TicketDTO, Ticket>();
                        cfg.CreateMap<TripDTO, Trip>();
                        cfg.CreateMap<CustomerDTO, Customer>().IncludeMembers();
                        cfg.CreateMap<ZipcodeDTO, Zipcode>();
                        cfg.CreateMap<CardDTO, CreditCard>();
                    });
                    var mapper = new Mapper(config);
                    var entity = mapper.Map<Customer>(dto);
                    return entity;
                }
                */

        //TODO: Remove if not needed
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

        /*
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
*/
    }
}