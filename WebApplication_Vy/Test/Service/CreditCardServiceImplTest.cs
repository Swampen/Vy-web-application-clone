using BLL.Service.Contracts;
using BLL.Service.Implementation;
using DAL.DTO;
using NUnit.Framework;
using Test.MockUtil;

namespace Test.Service
{
    [TestFixture]
    public class CreditCardServiceImplTest
    {
        private ICreditCardService _creditCardService;
        
        [Test]
        public void GetCreditCardTest()
        {
            _creditCardService = new CreditCardServiceImpl(CreditCardRepositoryMock.GetCreditCardMock());
            Assert.IsInstanceOf<CardDto>(_creditCardService.GetCreditCard(1));
        }

        [Test]
        public void GetCreditCardFromTicketIdTest()
        {
            _creditCardService = new CreditCardServiceImpl(CreditCardRepositoryMock.GetCreditCardFromTicketIdMock());
            Assert.IsInstanceOf<CardDto>(_creditCardService.GetCreditCardFromTicketId(1));
        }
    }
}