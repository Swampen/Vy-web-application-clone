using System.Collections.Generic;
using DAL.Db.Repositories.Contracts;
using DAL.DTO;
using MODEL.Models.Entities;
using Moq;

namespace Test.MockUtil.RepositoryMock
{
    public static class VyRepositoryMock
    {
        private static readonly CreditCard Card = new CreditCard();

        private static readonly List<Ticket> Tickets = new List<Ticket>
        {
            new Ticket {CreditCard = Card},
            new Ticket {CreditCard = Card}
        };

        private static readonly Zipcode Zipcode = new Zipcode {Postalcode = "2022", Postaltown = "Gjerdrum"};
        private static readonly Zipcode NullZipcode = null;

        private static readonly List<Customer> Customers = new List<Customer>
        {
            new Customer
            {
                Id = 1, Address = "Adress1", Email = "1@email.com", Givenname = "Given1", Surname = "Sur1",
                Tickets = Tickets, Zipcode = Zipcode
            },
            new Customer
            {
                Id = 2, Address = "Adress2", Email = "2@email.com", Givenname = "Given2", Surname = "Sur2",
                Tickets = Tickets, Zipcode = Zipcode
            },
            new Customer
            {
                Id = 3, Address = "Adress3", Email = "3@email.com", Givenname = "Given3", Surname = "Sur3",
                Tickets = Tickets, Zipcode = Zipcode
            }
        };

        public static IVyRepository GetAllCustomerDtos()
        {
            var mockRepo = new Mock<IVyRepository>();
            mockRepo.Setup(mock => mock.FindAllCustomers()).Returns(Customers);
            return mockRepo.Object;
        }

        public static IVyRepository CreateTicketMock()
        {
            var mockRepo = new Mock<IVyRepository>();
            mockRepo.Setup(mock => mock.CreateTicket(It.IsAny<Ticket>())).Returns(true);
            return mockRepo.Object;
        }

        public static IVyRepository DeleteTicketMock()
        {
            var mockRepo = new Mock<IVyRepository>();
            mockRepo.Setup(mock => mock.DeleteTicket(It.IsAny<int>())).Returns(true);
            return mockRepo.Object;
        }

        public static IVyRepository ChangeStationMock()
        {
            var mockRepo = new Mock<IVyRepository>();
            mockRepo.Setup(mock => mock.ChangeStation(It.IsAny<StationDTO>())).Returns(true);
            return mockRepo.Object;
        }

        public static IVyRepository FindZipCodeMock()
        {
            var mockRepo = new Mock<IVyRepository>();
            mockRepo.Setup(mock => mock.FindZipcode("0000")).Returns(Zipcode);
            mockRepo.Setup(mock => mock.FindZipcode("9999")).Returns(Zipcode);
            mockRepo.Setup(mock => mock.FindZipcode("1234")).Returns(NullZipcode);
            return mockRepo.Object;
        }
        
    }
}