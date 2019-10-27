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
        [TestCase("isSuper", true)]
        [TestCase("isSuper", false)]
        [TestCase("isNotSuper",false)]
        [TestCase("isNotSuper",true)]
        [TestCase("false",true)]
        [TestCase("false",false)]
        [TestCase("true",true)]
        [TestCase("true",false)]
        public void Login_shouldReturnViewThatsRedirectedToDefaultAdmin(string arg1, bool arg2)
        {
            //Arrange
            _loginController = new LoginController(LoginServiceMock.LoginMock())
            {
                ControllerContext = _controllerContext
            };
            _loginController.Session["Auth"] = arg2;
            
            var dto = new AdminUserDTO {Id = 1, Password = "test", Username = arg1, SuperAdmin = true};
            
            //Act
            var actionResult = (RedirectToRouteResult)_loginController.Login(dto);

            //Assert
            if (arg1.Equals("false"))
            {
                Assert.AreEqual("index", actionResult.RouteValues["Action"]);
            }
            else
            {
                Assert.AreEqual("stations", actionResult.RouteValues["Action"]);
                
            }
        }

        [Test]
        public void Login_isSuperAdmin_shouldReturnStationsView()
        {
            
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