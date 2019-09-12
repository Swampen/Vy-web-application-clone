using System.Web.Script.Serialization;
using Moq;
using NUnit.Framework;
using WebApplication_Vy.Controllers;
using WebApplication_Vy.Models.DTO;
using WebApplication_Vy.Service.Contracts;

namespace Test
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void SetUp()
        {
            _vyService = new Mock<IVyService>();
            _homeController = new HomeController(_vyService.Object);
        }

        private Mock<IVyService> _vyService;
        private HomeController _homeController;

        [Test]
        public void Test1()
        {
            var customerDto = new CustomerDTO();
            customerDto.Id = 1;
            customerDto.Givenname = "Fredrik";
            customerDto.Surname = "Frostad";
            customerDto.Address = "Adresse";
            var zipcodeDto = new ZipcodeDTO();
            zipcodeDto.Postalcode = "2022";
            zipcodeDto.Postaltown = "Gjerdrum";
            customerDto.ZipcodeDTO = zipcodeDto;
            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(customerDto);
            Assert.NotNull(_homeController.MakeCustomer(json));
        }
    }
}