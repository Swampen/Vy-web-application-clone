using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using BLL.Service.Contracts;
using BLL.Service.Implementation;
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
        public void Index_sessionShouldNotBeNull()
        {
            var controller = new HomeController(null, null, null);
            var context = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();

            context.Setup(s => s.HttpContext.Session).Returns(session.Object);
            controller.ControllerContext = context.Object;
            
            controller.Index();
            Assert.IsNull(controller.Session["AdminLogin"]);
        }
    }
}