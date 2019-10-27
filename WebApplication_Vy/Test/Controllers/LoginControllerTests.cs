using System.Web.Mvc;
using DAL.DTO;
using NUnit.Framework;
using Test.MockUtil;
using Test.MockUtil.ServiceMock;
using WebApplication_Vy.Controllers;

namespace Test.Controllers
{
    [TestFixture]
    public class LoginControllerTests
    {
        private LoginController _loginController;
        private ControllerContext _controllerContext;

        [SetUp]
        public void Setup()
        {
            _controllerContext = MockHttpSession.GetHttpSessionContext();
            _loginController = new LoginController(LoginServiceMock.LoginMock())
            {
                ControllerContext = _controllerContext
            };
            _loginController.Session["Auth"] = null;
            _loginController.Session["SuperAdmin"] = null;
            _loginController.Session["Username"] = null;
        }

        [TearDown]
        public void Teardown()
        {
            _loginController = null;
        }
        
        [Test]
        public void Login_shouldReturnViewThatsRedirectedToDefaultAdmin()
        {
            _loginController = new LoginController(LoginServiceMock.LoginMock())
            {
                ControllerContext = _controllerContext
            };
            _loginController.Session["Auth"] = true;

            var dto = new AdminUserDTO {Id = 1, Password = "test", Username = "test", SuperAdmin = true};
            
            var actionResult = (RedirectToRouteResult)_loginController.Login(dto);

            Assert.AreEqual("stations", actionResult.RouteValues["Action"]);
        }
        
        [Test]
        
        public void Login_shouldReturnUserView()
        {
            _loginController = new LoginController(LoginServiceMock.LoginMock())
            {
                ControllerContext = _controllerContext
            };
            var dto = new AdminUserDTO {Id = 1, Password = "admin", Username = "admin", SuperAdmin = true};
            _loginController.Session["Auth"] = false;

            var actionResult =(RedirectToRouteResult) _loginController.Login(dto);
           
            
            Assert.AreEqual("stations", actionResult.RouteValues["Action"]);
        }

        [Test]
        public void LogoutTest()
        {
            //Act
            var routeResult = (RedirectToRouteResult)_loginController.Logout();
            
            //Assert
            Assert.False((bool)_loginController.Session["Auth"]);
            Assert.False((bool)_loginController.Session["SuperAdmin"]);
            Assert.AreEqual("index", routeResult.RouteValues["Action"]);
        }
    }
}