using System.Collections.Generic;
using BLL.Service.Contracts;
using DAL.DTO;
using Moq;
using Moq.Language;

namespace Test.MockUtil.ServiceMock
{
    public class VyServiceMock
    {
        
        public static IVyService CreateTicketMock()
        {
            var mockService = new Mock<IVyService>();
            mockService.Setup(mock => mock.CreateTicket(It.IsAny<TicketDto>())).Returns(true);
            return mockService.Object;
        }

        public static IVyService GetCustomerDtos()
        {
            var mockService = new Mock<IVyService>();
            mockService.Setup(mock => mock.MaskCreditCardNumber(It.IsAny<CardDto>())).Verifiable();
            mockService.Setup(mock => mock
                    .GetCustomerDtos())
                .Returns(new List<CustomerDto> {new CustomerDto
                {
                    Address = "test",
                    Email = "test@test.no",
                    Givenname = "test",
                    Id = 1,
                    Surname = "test",
                    Tickets = new List<TicketDto>
                    {
                        new TicketDto
                        {
                            CreditCard = new CardDto
                            {
                                Card_Number = "2323 2323 2323 2323"
                            }
                        }
                    },
                    Zipcode = null
                }
                });
            
            return mockService.Object;
        }

        public static IVyService DeleteTicketMock()
        {
            var mockService = new Mock<IVyService>();
            mockService.Setup(mock => mock.DeleteTicket(1)).Returns(true);
            return mockService.Object;
        }
        
        public static IVyService UpdateCustomerMock()
        {
            var mockService = new Mock<IVyService>();
            mockService.Setup(mock => mock.UpdateCustomer(It.IsAny<CustomerDto>())).Returns(true);
            return mockService.Object;
        }

        public static IVyService DeleteCustomerMock()
        {
            var mockService = new Mock<IVyService>();
            mockService.Setup(mock => mock.DeleteCustomer(1)).Returns(true);
            return mockService.Object;
        }
    }
}