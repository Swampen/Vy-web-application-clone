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
        public void Index_AdminLoginShouldBeFalse()
        {
            //Arrange
            var controller = new HomeController(null, null, null);
            controller.ControllerContext = getHttpSessionContext();
            controller.Session["AdminLogin"] = null;
            
            //Act
            var actionResult = controller.Index();
            var viewResult = actionResult as ViewResult;
            
            //Assert
            Assert.IsFalse((bool)controller.Session["AdminLogin"]);
        }

        [Test]
        public void Index_HaveRoundTripsShouldBeFalse()
        {
            //Arrange
            var controller = new HomeController(null, null, null);
            controller.ControllerContext = getHttpSessionContext();
            controller.Session["HaveRoundTrip"] = null;
            
            //Act
            var actionResult = controller.Index();
            var viewResult = actionResult as ViewResult;
            
            //Assert
            Assert.IsFalse((bool)controller.Session["HaveRoundTrip"]);
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
            controller.ModelState.AddModelError("test", "test");
            
            TripQueryDTO tripQueryDto = new TripQueryDTO();
            
            //Act
            var actionResult = controller.Index(tripQueryDto);
            var viewResult = actionResult as ViewResult;
         
            //Assert
            Assert.IsNotNull(controller.Index());
            Assert.IsInstanceOf<ActionResult>(controller.Index());
            Assert.AreEqual("", viewResult.ViewName);
        }

        private ControllerContext getHttpSessionContext()
        {
            var context = new Mock<ControllerContext>();
            var session = new MockHttpSession();
            context.Setup(s => s.HttpContext.Session).Returns(session);
            return context.Object;
        }
    }
}