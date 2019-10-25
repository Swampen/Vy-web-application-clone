using DAL.Db.Repositories.Contracts;
using MODEL.Models.Entities;
using Moq;

namespace Test.MockUtil.RepositoryMock
{
    public class CustomerRepositoryMock
    {
        public static ICustomerRepository UpdateCustomer()
        {
            var mockRepo = new Mock<ICustomerRepository>();
            mockRepo.Setup(mock => mock.updateCustomer(It.IsAny<Customer>())).Returns(true);
            return mockRepo.Object;
        }

        public static ICustomerRepository DeleteCustomer()
        {
            var mockRepo = new Mock<ICustomerRepository>();
            mockRepo.Setup(mock => mock.deleteCustomer(It.Is<int>(i => i == 1))).Returns(true);
            mockRepo.Setup(mock => mock.deleteCustomer(It.Is<int>(i => i == 2))).Returns(true);
            return mockRepo.Object;
        }
    }
}