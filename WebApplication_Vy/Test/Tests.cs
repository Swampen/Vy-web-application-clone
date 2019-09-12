using System;
using System.Runtime.InteropServices;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Razor.Text;
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
        private Mock<IVyService> _vyService;
        private HomeController _homeController;

        [SetUp]
        public void SetUp()
        {
            _vyService = new Mock<IVyService>();
            _homeController = new HomeController(_vyService.Object);
        }
        
        [Test]
        public void Test1()
        {
            CustomerDTO customerDto = new CustomerDTO();
            customerDto.Id = 1;
            customerDto.Givenname = "Fredrik";
            customerDto.Surname = "Frostad";
            customerDto.Address = "Adresse";
            customerDto.Zipcode = 2022;
            var serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(customerDto);
            Assert.NotNull(_homeController.MakeCustomer(json));
        }

        [Test]
        public void testAM()
        {

        }
    }
}