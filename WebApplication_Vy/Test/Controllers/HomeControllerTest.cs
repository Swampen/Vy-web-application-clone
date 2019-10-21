using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using BLL.Service.Contracts;
using BLL.Service.Implementation;
using DAL.DTO.TripData;
using Moq;
using NUnit.Framework;
using Test.MockUtil;
using WebApplication_Vy.Controllers;

namespace Test.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        private readonly IVyService _vyService;
        private readonly IZipSearchService _zipSearch;
        private readonly IStationService _stationService;
        
        [SetUp]
        public void Setup()
        {
            
        }
        
        [Test]
        public void Index_shouldReturnIndex()
        {
            //Arrange
            var controller = new HomeController(null, null, null);
            var context = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();

            context.Setup(s => s.HttpContext.Session).Returns(session.Object);
            controller.ControllerContext = context.Object;
            
            //Act
            var actionResult = controller.Index();
            var viewResult = actionResult as ViewResult;
         
            //Assert
            Assert.IsNotNull(controller.Index());
            Assert.IsInstanceOf<ActionResult>(controller.Index());
            Assert.AreEqual("", viewResult.ViewName);
        }
        
        [Test]
        public void Index_invalidModelState_shouldReturnIndex()
        {
            //Arrange
            var controller = new HomeController(null, null, null);
            var context = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();

            context.Setup(s => s.HttpContext.Session).Returns(session.Object);
            controller.ControllerContext = context.Object;
            
            TripQueryDTO tripQueryDto = new TripQueryDTO();
            
            //Act
            var actionResult = controller.Index(tripQueryDto);
            var viewResult = actionResult as ViewResult;
         
            //Assert
            Assert.IsNotNull(controller.Index());
            Assert.IsInstanceOf<ActionResult>(controller.Index());
            Assert.AreEqual("", viewResult.ViewName);
        }
    }
}