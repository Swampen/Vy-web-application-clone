using NUnit.Framework;
using Test.MockUtil;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Db.Repositories.Implementation;
using WebApplication_Vy.Models.DTO;
using WebApplication_Vy.Service.Contracts;
using WebApplication_Vy.Service.Implementation;

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