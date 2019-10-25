using System.Web.Mvc;
using BLL.Service.Contracts;
using DAL.DTO;
using NUnit.Framework;
using Test.MockUtil;
using Test.MockUtil.ServiceMock;
using WebApplication_Vy.Controllers;

namespace Test.Controllers
{
    [TestFixture]
    public class AdminControllerTest
    {
        private AdminController _adminController;
        private ControllerContext _controllerContext;

        [SetUp]
        public void Setup()
        {
            _controllerContext = MockHttpSession.GetHttpSessionContext();
            _adminController = new AdminController(
                null,
                null,
                null)
            {
                ControllerContext = _controllerContext
            };
            _adminController.Session["Auth"] = null;
            _adminController.Session["SuperAdmin"] = null;
        }

        [TearDown]
        public void TearDown()
        {
            _adminController = null;
        }

        [Test]
        public void Tickets_shouldReturnCustomersView()
        {
            //Arrange
            _adminController = new AdminController(VyServiceMock.GetCustomerDtos(), null, null)
            {
                ControllerContext = _controllerContext
            };
            _adminController.Session["Auth"] = true;
            
            //Act
            var actionResult = _adminController.Tickets();
            var viewResult = actionResult as ViewResult;
            
            //Assert
            Assert.AreEqual("customers", viewResult.ViewName);
        }

        [Test]
        public void Tickets_shouldReturnUserIndexView()
        {
            //Arrange
            _adminController = new AdminController(VyServiceMock.GetCustomerDtos(), null, null)
            {
                ControllerContext = _controllerContext
            };
            _adminController.Session["Auth"] = false;
            
            //Act
            var routeResult = (RedirectToRouteResult)_adminController.Tickets();
            
            //Assert
            Assert.AreEqual("index", routeResult.RouteValues["Action"]);
        }

        [Test]
        public void DeleteTicket_shouldReturnTicketsView()
        {
            //Arrange
            _adminController = new AdminController(VyServiceMock.GetCustomerDtos(), null, null)
            {
                ControllerContext = _controllerContext
            };
            _adminController.Session["Auth"] = true;
            
            //Act
            var routeResult = (RedirectToRouteResult)_adminController.DeleteTicket(1);
    
            
            //Assert
            Assert.AreEqual("Tickets", routeResult.RouteValues["Action"]);
        }

        [Test]
        public void DeleteTicket_shouldReturnUserIndexView()
        {
            //Arrange
            _adminController = new AdminController(VyServiceMock.GetCustomerDtos(), null, null)
            {
                ControllerContext = _controllerContext
            };
            _adminController.Session["Auth"] = false;
            
            //Act
            var routeResult = (RedirectToRouteResult)_adminController.DeleteTicket(1);
    
            
            //Assert
            Assert.AreEqual("index", routeResult.RouteValues["Action"]);
        }

        [Test]
        public void Stations_shouldReturnStationsView()
        {
            //Arrange
            _adminController = new AdminController(null, StationServiceMock.GetAllStations(), null)
            {
                ControllerContext = _controllerContext
            };
            _adminController.Session["Auth"] = true;
            
            //Act
            var actionResult = _adminController.Stations();
            var viewResult = actionResult as ViewResult;
            
            //Assert
            Assert.AreEqual("", viewResult.ViewName);
        }

        [Test]
        public void Stations_shouldReturnUserIndexView()
        {
            //Arrange
            _adminController = new AdminController(null, StationServiceMock.GetAllStations(), null)
            {
                ControllerContext = _controllerContext
            };
            _adminController.Session["Auth"] = false;
            
            //Act
            var routeResult = (RedirectToRouteResult)_adminController.Stations();
    
            
            //Assert
            Assert.AreEqual("index", routeResult.RouteValues["Action"]);
        }

        [Test]
        public void UpdateStations_shouldReturnStationsView()
        {
            //Arrange
            _adminController = new AdminController(null, StationServiceMock.ChangeStationMock(), null)
            {
                ControllerContext = _controllerContext
            };
            _adminController.Session["Auth"] = true;
            
            //Act
            var routeResult = (RedirectToRouteResult)_adminController.UpdateStation(new StationDTO());
            
            //Assert
            Assert.AreEqual("stations", routeResult.RouteValues["Action"]);
        }

        [Test]
        public void UpdateStations_shouldReturnUserIndexView()
        {
            //Arrange
            _adminController = new AdminController(null, StationServiceMock.ChangeStationMock(), null)
            {
                ControllerContext = _controllerContext
            };
            _adminController.Session["Auth"] = false;
            
            //Act
            var routeResult = (RedirectToRouteResult)_adminController.UpdateStation(new StationDTO());
            
            //Assert
            Assert.AreEqual("index", routeResult.RouteValues["Action"]);
        }

        [Test]
        public void Customers_shouldReturnCustomersView()
        {
            //Arrange
            _adminController = new AdminController(VyServiceMock.GetCustomerDtos(), null, null)
            {
                ControllerContext = _controllerContext
            };
            _adminController.Session["Auth"] = true;
            
            //Act
            var actionResult = _adminController.Customers();
            var viewResult = actionResult as ViewResult;
            
            //Assert
            Assert.AreEqual("", viewResult.ViewName);
        }
        
        [Test]
        public void Customers_shouldReturnUserIndexView()
        {
            //Arrange
            _adminController = new AdminController(VyServiceMock.GetCustomerDtos(), null, null)
            {
                ControllerContext = _controllerContext
            };
            _adminController.Session["Auth"] = false;
            
            //Act
            var routeResult = (RedirectToRouteResult)_adminController.Customers();
            
            //Assert
            Assert.AreEqual("index", routeResult.RouteValues["Action"]);
        }
        
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void UpdateCustomer_shouldReturnCustomersView(bool value)
        {
            //Arrange
            _adminController = new AdminController(VyServiceMock.UpdateCustomerMock(), null, null)
            {
                ControllerContext = _controllerContext
            };
            _adminController.Session["Auth"] = true;
            if (value)
            {
                _adminController.ModelState.AddModelError("test", "test");    
            }
            
            //Act
            var routeResult = (RedirectToRouteResult)_adminController.UpdateCustomer(new CustomerDto());
            
            //Assert
            Assert.AreEqual("Customers", routeResult.RouteValues["Action"]);
        }
        
        [Test]
        public void UpdateCustomer_shouldReturnUserIndexView()
        {
            //Arrange
            _adminController = new AdminController(VyServiceMock.UpdateCustomerMock(), null, null)
            {
                ControllerContext = _controllerContext
            };
            _adminController.Session["Auth"] = false;
            
            //Act
            var routeResult = (RedirectToRouteResult)_adminController.UpdateCustomer(new CustomerDto());
            
            //Assert
            Assert.AreEqual("index", routeResult.RouteValues["Action"]);
        }       
        
        [Test]
        public void DeleteCustomer_shouldReturnCustomersView()
        {
            //Arrange
            _adminController = new AdminController(VyServiceMock.DeleteCustomerMock(), null, null)
            {
                ControllerContext = _controllerContext
            };
            _adminController.Session["Auth"] = true;
            
            //Act
            var routeResult = (RedirectToRouteResult)_adminController.DeleteCustomer(1);
            
            //Assert
            Assert.AreEqual("Customers", routeResult.RouteValues["Action"]);
        }
        
        [Test]
        public void DeleteCustomer_shouldReturnUserIndexView()
        {
            //Arrange
            _adminController = new AdminController(VyServiceMock.DeleteCustomerMock(), null, null)
            {
                ControllerContext = _controllerContext
            };
            _adminController.Session["Auth"] = false;
            
            //Act
            var routeResult = (RedirectToRouteResult)_adminController.DeleteCustomer(1);
            
            //Assert
            Assert.AreEqual("Index", routeResult.RouteValues["Action"]);
        }       
        
        [Test]
        public void Admins_shouldReturnAdminsView()
        {
            //Arrange
            _adminController = new AdminController(null, null, LoginServiceMock.GetAllAdminsMock())
            {
                ControllerContext = _controllerContext
            };
            _adminController.Session["Auth"] = true;
            
            //Act
            var actionResult = _adminController.Admins();
            var viewResult = actionResult as ViewResult;
            
            //Assert
            Assert.AreEqual("", viewResult.ViewName);
        }
        
        [Test]
        public void Admins_shouldReturnUserIndexView()
        {
            //Arrange
            _adminController = new AdminController(null, null, LoginServiceMock.GetAllAdminsMock())
            {
                ControllerContext = _controllerContext
            };
            _adminController.Session["Auth"] = false;
            
            //Act
            var routeResult = (RedirectToRouteResult)_adminController.Admins();
            
            //Assert
            Assert.AreEqual("index", routeResult.RouteValues["Action"]);
        }
        
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void DeleteAdmin_shouldReturnCustomersView(bool value)
        {
            //Arrange
            _adminController = new AdminController(null, null, LoginServiceMock.DeleteAdminMock())
            {
                ControllerContext = _controllerContext
            };
            _adminController.Session["SuperAdmin"] = value;
            
            //Act
            var routeResult = (RedirectToRouteResult)_adminController.DeleteAdmin(1);
            
            //Assert
            Assert.AreEqual("admins", routeResult.RouteValues["Action"]);
        }

        [Test]
        public void RegisterNewAdmin_shouldReturnAdminsView()
        {
            //Needs several testcases
            Assert.Fail();
        }
    }
}