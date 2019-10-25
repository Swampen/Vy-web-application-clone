using System.Web.Mvc;
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
        public void LoginControllerTest()
        {
            Assert.Fail();
        }

        [Test]
        public void LogoutTest()
        {
            //Act
            var routeResult = (RedirectToRouteResult)_loginController.Logout();
            
            //Assert
            Assert.False((bool)_loginController.Session["Auth"]);
            Assert.False((bool)_loginController.Session["SuperAdmin"]);
            Assert.AreEqual("index", routeResult.RouteValues["" +
                                                             "Action"]);
        }

        [Test]
        public void LoginTest()
        {
            Assert.Fail();
        }
    }
}