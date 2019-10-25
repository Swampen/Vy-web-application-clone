using DAL.DTO;
using DAL.DTO.TripData;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Test.MockUtil;
using Test.MockUtil.ServiceMock;
using WebApplication_Vy.Controllers;

namespace Test.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        private HomeController _homeController;

        [SetUp]
        public void Setup()
        {
            _homeController = new HomeController(null, null, null)
            {
                ControllerContext = MockHttpSession.GetHttpSessionContext()
            };
            _homeController.Session["HaveRoundTrip"] = null;
            _homeController.Session["ChosenTrip"] = null;
            _homeController.Session["ChosenTrips"] = null;
            _homeController.Session["ToTrip"] = null;
            _homeController.Session["ReturnTripQuery"] = null;
            _homeController.Session["Auth"] = null;
        }

        [TearDown]
        public void destroy()
        {
            _homeController = null;
        }

        [TestCase("")]
        [TestCase("0")]
        [TestCase("000")]
        [TestCase("00")]
        [TestCase("0x00")]
        [TestCase("xxxx")]
        [TestCase("x000")]
        [TestCase("x")]
        public void SearchZip_shouldReturnEmptyString(string value)
        {
            //Arrange
            _homeController = new HomeController(
                null,
                ZipSearchServiceMock.GetPostalTownMock(),
                null);
            var zipcodeDto = new ZipcodeDto
            {
                Postalcode = value,
                Postaltown = "test"
            };

            //Act
            var actual = _homeController.SearchZip(zipcodeDto);

            //Assert
            Assert.AreEqual("", actual);
        }

        [Test]
        public void GetAllStations_shouldReturnStringOfStations()
        {
            //Arrange
            _homeController = new HomeController(
                null,
                null,
                StationServiceMock.GetAllKeyValueStations());
            //Act
            var stations = _homeController.GetAllStations();
            var serializer = new JavaScriptSerializer();
            var actual = serializer.Deserialize<Dictionary<string, string>>(stations);

            //Assert
            Assert.AreEqual("test", actual["test"]);
        }

        [Test]
        public void Index_AdminLoginShouldBeFalse()
        {
            //Act
            var actionResult = _homeController.Index();

            //Assert
            Assert.IsFalse((bool)_homeController.Session["Auth"]);
        }

        [Test]
        public void Index_POST_HaveRoundTripsShouldBeFalse()
        {
            //Act
            var actionResult = _homeController.Index();
            var viewResult = actionResult as ViewResult;

            //Assert
            Assert.IsFalse((bool)_homeController.Session["HaveRoundTrip"]);
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
        public void index_POST_returntripqueryShouldBeSet()
        {
            //Arrange
            var tripQueryDto = new TripQueryDTO
            {
                Round_Trip = true,
                Adult = 1,
                Arrival_Station = "arrivalTest",
                Arrival_StationId = "arrId",
                Child = 1,
                Date = "dateTest",
                Departure_Station = "departureTest",
                Departure_StationId = "depId",
                Return_Date = "dateReturnTest",
                Return_Time = "returnTimeTest",
                Student = 1,
                Senior = 1,
                Time = "timeTest"
            };

            //Act
            _homeController.Index(tripQueryDto);
            var resultTripqueryDto = (TripQueryDTO)_homeController.Session["ReturnTripQuery"];

            //Assert
            Assert.AreEqual(tripQueryDto.Adult, resultTripqueryDto.Adult);
            Assert.AreEqual(tripQueryDto.Arrival_Station, resultTripqueryDto.Departure_Station);
            Assert.AreEqual(tripQueryDto.Arrival_StationId, resultTripqueryDto.Departure_StationId);
            Assert.AreEqual(tripQueryDto.Child, resultTripqueryDto.Child);
            Assert.AreEqual(tripQueryDto.Departure_Station, resultTripqueryDto.Arrival_Station);
            Assert.AreEqual(tripQueryDto.Departure_StationId, resultTripqueryDto.Arrival_StationId);
            Assert.AreEqual(null, resultTripqueryDto.Return_Date);
            Assert.AreEqual(null, resultTripqueryDto.Return_Time);
            Assert.AreEqual(tripQueryDto.Student, resultTripqueryDto.Student);
            Assert.AreEqual(tripQueryDto.Senior, resultTripqueryDto.Senior);
            Assert.AreEqual(tripQueryDto.Return_Time, resultTripqueryDto.Time);
            Assert.AreEqual(null, resultTripqueryDto.Return_Date);
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
        public void RegisterTicket_POST_shouldReturnCustomerDetailsView()
        {
            //Arrange
            _homeController.ModelState.AddModelError("test", "test");
            var submitPurchaseDto = new SubmitPurchaseDto();

            //Act
            var actionResult = _homeController.RegisterTicket(submitPurchaseDto);
            var viewResult = actionResult as ViewResult;

            //Assert
            Assert.AreEqual("CustomerDetails", viewResult.ViewName);
        }

        [Test]
        public void RegisterTicket_shouldRedirectToIndex()
        {
            //Arrange
            var vyService = VyServiceMock.CreateTicketMock();
            _homeController = new HomeController(vyService, null, null);
            var submitPurchaseDto = new SubmitPurchaseDto();
            submitPurchaseDto.ReturnTripTicket = new TicketDto();
            submitPurchaseDto.TripTicket = new TicketDto();
            submitPurchaseDto.TripTicket.Customer = new CustomerDto();
            submitPurchaseDto.TripTicket.CreditCard = new CardDto();
            submitPurchaseDto.ReturnTripTicket.ArrivalStation = "test";

            //Act
            var actionResult = (RedirectToRouteResult)_homeController.RegisterTicket(submitPurchaseDto);

            //Assert
            Assert.AreEqual("Index", actionResult.RouteValues["action"]);
        }

        [Test]
        public void RegisterTicket_shouldReturnCustomerDetailsView()
        {
            //Arrange
            _homeController.ModelState.AddModelError("test", "test");
            var submitPurchaseDto = new SubmitPurchaseDto();
            _homeController.Session["ChosenTrips"] = new List<TripDTO>
            {
                new TripDTO()
            };

            //Act
            var actionResult = _homeController.RegisterTicket(submitPurchaseDto);
            var viewResult = actionResult as ViewResult;

            //Assert
            Assert.AreEqual("CustomerDetails", viewResult.ViewName);
        }

        [Test]
        public void RegisterTicket_viewbagShouldBeSet()
        {
            //Arrange
            _homeController.ModelState.AddModelError("test", "test");
            var submitPurchaseDto = new SubmitPurchaseDto();
            _homeController.Session["ChosenTrips"] = new List<TripDTO>
            {
                new TripDTO()
            };

            //Act
            _homeController.RegisterTicket(submitPurchaseDto);

            //Assert
            Assert.IsNotNull(_homeController.ViewData["Model"]);
            Assert.IsInstanceOf<List<TripDTO>>(_homeController.ViewData["Model"]);
        }

        [Test]
        public void SearchZip_shouldReturnResult()
        {
            //Arrange
            _homeController = new HomeController(
                null,
                ZipSearchServiceMock.GetPostalTownMock(),
                null);
            var zipcodeDto = new ZipcodeDto
            {
                Postalcode = "0000",
                Postaltown = "test"
            };

            //Act
            var actual = _homeController.SearchZip(zipcodeDto);

            //Assert
            Assert.AreEqual("test", actual);
        }

        [Test]
        public void Trips_GET_sessionChosenTripsShouldNotBeNull()
        {
            //Arrrange
            var tripQueryDto = new TripQueryDTO();

            //Act
            _homeController.Trips(tripQueryDto);

            //Assert
            Assert.IsInstanceOf<List<TripDTO>>(_homeController.Session["ChosenTrips"]);
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
        public void Trips_POST_toTripDtoShouldBeSet()
        {
            //Arrange
            _homeController.Session["HaveRoundTrip"] = true;
            var expectedTripDto = new TripDTO
            {
                Date = "Test",
                Duration = "Test",
                Round_Trip = false
            };

            //Act
            _homeController.Trips(expectedTripDto);
            var actual = (List<TripDTO>)_homeController.Session["ChosenTrips"];

            //Assert
            Assert.AreEqual(2, actual.Count);
        }
    }
}