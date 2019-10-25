using System.Collections.Generic;
using AutoMapper;
using BLL.Service.Contracts;
using DAL.Db.Repositories.Contracts;
using DAL.DTO;
using DAL.DTO.TripData;
using MODEL.Models.Entities;
using MODEL.Models.Entities.TripData;

namespace BLL.Service.Implementation
{
    public class VyServiceImpl : IVyService
    {
        private readonly IVyRepository _vyRepository;
        private readonly ICustomerRepository _customerRepository;


        public VyServiceImpl(IVyRepository vyRepository, ICustomerRepository customerRepository)
        {
            _vyRepository = vyRepository;
            _customerRepository = customerRepository;
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
                Tickets = ticketDtos
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
                TrainChanges = ticket.TrainChanges
            };
        }

        private CardDto mapCardDto(CreditCard card)
        {
            return new CardDto
            {
                Card_Number = card.CreditCardNumber,
                Card_Holder = card.CardholderName,
                Cvc = card.Cvc
            };
        }

        private Ticket MapTicketEntity(TicketDto dto)
        {
            var ticket = new Ticket();
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
            var customer = new Customer
            {
                Id = dto.Id,
                Givenname = dto.Givenname,
                Surname = dto.Surname,
                Address = dto.Address,
                Zipcode = mapZipCodeEntity(dto.Zipcode),
                Email = dto.Email
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
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ZipcodeDto, Zipcode>(); });
            var mapper = new Mapper(config);
            return mapper.Map<Zipcode>(dto);
        }

        public bool UpdateCustomer(CustomerDto customer)
        {
            return _customerRepository.updateCustomer(MapCustomerEntity(customer));
        }

        public bool DeleteCustomer(int id)
        {
            return _customerRepository.deleteCustomer(id);
        }
    }
}