using DAL.Db.Repositories.Contracts;
using MODEL.Models.Entities;
using Moq;

namespace Test.MockUtil.RepositoryMock
{
    public static class CreditCardRepositoryMock
    {
        private static readonly CreditCard Card = new CreditCard
        {
            Id = 1,
            CardholderName = "Cardholder",
            CreditCardNumber = "1111 2222 3333 4444",
            Cvc = "123"
        };

        public static ICreditCardRepository GetCreditCardMock()
        {
            var mockRepo = new Mock<ICreditCardRepository>();
            mockRepo.Setup(mock => mock.GetCardById(It.IsAny<int>())).Returns(Card);
            return mockRepo.Object;
        }

        public static ICreditCardRepository GetCreditCardFromTicketIdMock()
        {
            var mockRepo = new Mock<ICreditCardRepository>();
            mockRepo.Setup(mock => mock.GetCardByTicketId(It.IsAny<int>())).Returns(Card);
            return mockRepo.Object;
        }
    }
}