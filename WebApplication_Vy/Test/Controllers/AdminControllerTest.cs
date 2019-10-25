using BLL.Service.Contracts;
using NUnit.Framework;
using Test.MockUtil.ServiceMock;
using WebApplication_Vy.Controllers;

namespace Test.Controllers
{
    [TestFixture]
    public class AdminControllerTest
    {
        private AdminController _adminController;
        
        [SetUp]
        public void Setup()
        {
            _adminController = new AdminController(
                null,
                null,
                null);
        }

        [TearDown]
        public void TearDown()
        {
            _adminController = null;
            
        }
        
        [Test]
        public void Index_shouldReturnAdminIndexView()
        {
            //Auth is set and true
            Assert.Fail();
        }

        [Test]
        public void Index_shouldReturnUserIndexView()
        {
            //Auth is null or false
            Assert.Fail();
        }

        [Test]
        public void Tickets_shouldReturnCustomersView()
        {
            //Auth is set and true
            Assert.Fail();
        }

        [Test]
        public void Tickets_shouldReturnUserIndexView()
        {
            //Auth is null or false
            Assert.Fail();
        }

        [Test]
        public void DeleteTicket_shouldReturnTicketsView()
        {
            //Auth is set and true
            Assert.Fail();
        }

        [Test]
        public void DeleteTicket_shouldReturnUserIndexView()
        {
            //Auth is null or false
            Assert.Fail();
        }

        [Test]
        public void Stations_shouldReturnStationsView()
        {
            //Auth is set and true
            Assert.Fail();
        }

        [Test]
        public void Stations_shouldReturnUserIndexView()
        {
            // Auth is null or false
            Assert.Fail();
        }

        [Test]
        public void UpdateStations_shouldReturnStationsView()
        {
            //Auth is set and true
            Assert.Fail();
        }

        [Test]
        public void UpdateStations_shouldReturnUserIndexView()
        {
            //Auth is null or false
            Assert.Fail();
        }

        [Test]
        public void Customers_shouldReturnCustomersView()
        {
            //Auth is set and true
            Assert.Fail();
        }
        
        [Test]
        public void Customers_shouldReturnUserIndexView()
        {
            // Auth is null or false
            Assert.Fail();
        }
        
        [Test]
        public void UpdateCustomer_shouldReturnCustomersView()
        {
            //Auth is set and true
            Assert.Fail();
        }
        
        [Test]
        public void UpdateCustomer_shouldReturnUserIndexView()
        {
            // Auth is null or false
            Assert.Fail();
        }       
        
        [Test]
        public void DeleteCustomer_shouldReturnCustomersView()
        {
            //Auth is set and true
            Assert.Fail();
        }
        
        [Test]
        public void DeleteCustomer_shouldReturnUserIndexView()
        {
            // Auth is null or false
            Assert.Fail();
        }       
        
        [Test]
        public void Admins_shouldReturnAdminsView()
        {
            //Auth is set and true
            Assert.Fail();
        }
        
        [Test]
        public void Admins_shouldReturnUserIndexView()
        {
            // Auth is null or false
            Assert.Fail();
        }
        
        [Test]
        public void DeleteAdmin_shouldReturnCustomersView()
        {
            //SuperAdmin is set and true
            Assert.Fail();
        }

        [Test]
        public void RegisterNewAdmin_shouldReturnAdminsView()
        {
            //Needs several testcases
            Assert.Fail();
        }
    }
}