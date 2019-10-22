using System;
using BLL.Service.Contracts;
using BLL.Service.Implementation;
using DAL.Db.Repositories.Implementation;
using DAL.DTO;
using NUnit.Framework;
using NUnit.Framework.Internal.Commands;
using Test.MockUtil;
using WebApplication_Vy.Controllers;

namespace Test.Service
{
    [TestFixture]
    public class VyServiceImplTest
    {
        private IVyService _service;
        private CardDto _cardDto;
        private CustomerDto _customerDto;
        private TicketDto _ticketDto;
        private ZipcodeDto _zipcodeDto;
        
        [SetUp]
        public void SetUp()
        {
            _cardDto = new CardDto
            {
                Card_Holder = "TestHolder",
                Card_Number = "1111 2222 3333 4444",
                Cvc = "123",
                Expiry_Date = "01-01-2000"
            };
            
            _zipcodeDto = new ZipcodeDto
            {
                Postalcode = "2022",
                Postaltown = "Gjerdrum"
            };
            
            _customerDto = new CustomerDto
            {
                Address = "Address",
                Email = "mail@mail.com",
                Givenname = "Given",
                Surname = "Sur",
                Id = 1,
                Tickets = null,
                Zipcode = _zipcodeDto,
            };
            
            _ticketDto = new TicketDto
            {
                ArrivalStation = "Arrival",
                CreditCard = _cardDto,
                ArrivalTime = "Arrival",
                Customer = _customerDto,
                DepartureStation = "Departure",
                DepartureTime = "DepartureTime",
                Duration = "Duration",
                Id = 1,
                Price = 100,
                TrainChanges = ""
            };
        }

        [TearDown]
        public void TearDown()
        {
            _service = null;
            _cardDto = null;
            _customerDto = null;
            _ticketDto = null;
            _zipcodeDto = null;
        }

        [Test]
        public void MaskCreditCardNumberTest()
        {
            _service = new VyServiceImpl(new VyRepositoryImpl());
            string before = _cardDto.Card_Number;
            string expected = "**** **** **** 4444";
            _service.MaskCreditCardNumber(_cardDto);
            Assert.AreNotEqual(before, _cardDto.Card_Number);
            Assert.AreEqual(expected, _cardDto.Card_Number);
        }

        [Test]
        public void GetCustomerDtosTest()
        {
            _service = new VyServiceImpl(VyRepositoryMock.GetAllCustomerDtos());
            Assert.AreEqual(3, _service.GetCustomerDtos().Count);
            Assert.NotNull(_service.GetCustomerDtos().Find(dto => dto.Id == 1));
        }
        
        [Test]
        public void GetTicketDtosTest()
        {
            _service = new VyServiceImpl(VyRepositoryMock.GetAllCustomerDtos());
            Assert.IsInstanceOf(typeof(TicketDto), _service.GetTicketDtos()[0]);
            Assert.AreEqual(6, _service.GetTicketDtos().Count);
        }

        [Test]
        public void CreateTicketTest()
        {
            _service = new VyServiceImpl(VyRepositoryMock.CreateTicketMock());
            Assert.IsTrue(_service.CreateTicket(_ticketDto));
        }

        [Test]
        public void DeleteTicketTest()
        {
            _service = new VyServiceImpl(VyRepositoryMock.DeleteTicketMock());
            Assert.IsTrue(_service.DeleteTicket(1));
        }
    }
}