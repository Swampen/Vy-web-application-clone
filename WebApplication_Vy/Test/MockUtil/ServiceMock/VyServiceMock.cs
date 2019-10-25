using BLL.Service.Contracts;
using DAL.DTO;
using Moq;

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
    }
}