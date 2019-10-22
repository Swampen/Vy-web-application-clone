using System.Web;
using System.Web.Mvc;
using BLL.Service.Contracts;
using DAL.DTO;
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
        [SetUp]
        public void Setup()
        {
            _homeController = new HomeController(null, null, null)
            {
                ControllerContext = getHttpSessionContext()
            };
            _homeController.Session["AdminLogin"] = null;
            _homeController.Session["HaveRoundTrip"] = null;
            _homeController.Session["ChosenTrip"] = null;
            _homeController.Session["ToTrip"] = null;
            _homeController.Session["ReturnTripQuery"] = null;
        }

        [TearDown]
        public void destroy()
        {
            _homeController = null;
        }

        private readonly IVyService _vyService;
        private readonly IZipSearchService _zipSearch;
        private readonly IStationService _stationService;
        private HomeController _homeController;

        private ControllerContext getHttpSessionContext()
        {
            var context = new Mock<ControllerContext>();
            var session = new MockHttpSession();
            context.Setup(s => s.HttpContext.Session).Returns(session);
            return context.Object;
        }

        [Test]
        public void Index_AdminLoginShouldBeFalse()
        {
            //Act
            var actionResult = _homeController.Index();

            //Assert
            Assert.IsFalse((bool) _homeController.Session["AdminLogin"]);
        }

        [Test]
        public void Index_POST_HaveRoundTripsShouldBeFalse()
        {
            //Act
            var actionResult = _homeController.Index();
            var viewResult = actionResult as ViewResult;

            //Assert
            Assert.IsFalse((bool) _homeController.Session["HaveRoundTrip"]);
        }

        [Test]
        public void Index_POST_invalidModelState_shouldReturnIndex()
        {
            //Arrange
            _homeController.ModelState.AddModelError("test", "test");

            var tripQueryDto = new TripQueryDTO();

            //Act
            var actionResult = _homeController.Index(tripQueryDto);
            var viewResult = actionResult as ViewResult;

            //Assert
            Assert.IsInstanceOf<ActionResult>(_homeController.Index());
            Assert.AreEqual("", viewResult.ViewName);
        }

        [Test]
        public void Index_POST_shouldReturnIndex()
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
        public void Index_POST_shouldReturnTrips()
        {
            //Arrange
            var tripQueryDto = new TripQueryDTO();

            //Act
            var actionResult = _homeController.Index(tripQueryDto);
            var viewResult = actionResult as ViewResult;

            //Assert
            Assert.IsInstanceOf<ActionResult>(_homeController.Index());
            Assert.AreEqual("Trips", viewResult.ViewName);
        }

        [Test]
        public void Trips_POST_sessionChoosenTripsShouldNotBeNull()
        {
            //Arrrange
            var tripDto = new TripDTO();
            tripDto.Round_Trip = false;
            _homeController.Session["HaveRoundTrip"] = false;

            //Act
            _homeController.Trips(tripDto);

            //Assert
            Assert.IsNotNull(_homeController.Session["ChosenTrips"]);
        }

        [Test]
        public void Trips_POST_sessionToTripShouldBeNull()
        {
            //Arrrange
            var tripDto = new TripDTO();
            tripDto.Round_Trip = false;
            _homeController.Session["HaveRoundTrip"] = false;

            //Act
            _homeController.Trips(tripDto);

            //Assert
            Assert.IsNull(_homeController.Session["ToTrip"]);
        }

        [Test]
        public void Trips_POST_shouldReturnCustomerDetailsView()
        {
            //Arrrange
            var tripDto = new TripDTO();
            tripDto.Round_Trip = false;
            _homeController.Session["HaveRoundTrip"] = false;

            //Act
            var actionResult = _homeController.Trips(tripDto);
            var viewResult = actionResult as ViewResult;

            //Assert
            Assert.IsNull(_homeController.Session["ToTrip"]);
            Assert.AreEqual("CustomerDetails", viewResult.ViewName);
        }

        [Test]
        public void Trips_POST_shouldReturnTripsView()
        {
            //Arrrange
            var tripDto = new TripDTO();
            tripDto.Round_Trip = true;
            _homeController.Session["HaveRoundTrip"] = true;

            //Act
            var actionResult = _homeController.Trips(tripDto);
            var viewResult = actionResult as ViewResult;

            //Assert
            Assert.AreEqual("", viewResult.ViewName);
        }

        [Test]
        public void Trips_GET_shouldReturnTripsView()
        {
            //Arrrange
            var tripQueryDto = new TripQueryDTO();

            //Act
            var actionResult = _homeController.Trips(tripQueryDto);
            var viewResult = actionResult as ViewResult;

            //Assert
            Assert.AreEqual("", viewResult.ViewName);
        }

        [Test]
        public void Trips_GET_sessionChosenTripsShouldNotBeNull()
        {
            //Arrrange
            var tripQueryDto = new TripQueryDTO();

            //Act
           _homeController.Trips(tripQueryDto);

            //Assert
            Assert.IsInstanceOf<System.Collections.Generic.List<TripDTO>>(_homeController.Session["ChosenTrips"]);
        } 
        
        [Test]
        public void RegisterTicket_POST_shouldReturnCustomerDetailsView()
        {
            //Arrange
            _homeController.ModelState.AddModelError("test", "test");
            SubmitPurchaseDto submitPurchaseDto = new SubmitPurchaseDto();
            
            //Act
            var actionResult = _homeController.RegisterTicket(submitPurchaseDto);
            var viewResult = actionResult as ViewResult;

            //Assert
            Assert.AreEqual("", viewResult.ViewName);
        }

        [Test]
        public void RegisterTicket_shouldRedirectToIndex()
        {
            //Arrange
           // var vyService = new Mock<>();            
            SubmitPurchaseDto submitPurchaseDto = new SubmitPurchaseDto();
            
            //Act
            var actionResult = _homeController.RegisterTicket(submitPurchaseDto);
            var viewResult = actionResult as ViewResult;

            //Assert
           // Assert.AreEqual("Index", actionResult.ToString());
        }
    }
}