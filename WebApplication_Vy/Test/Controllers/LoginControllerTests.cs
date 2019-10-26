using NUnit.Framework;
using WebApplication_Vy.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Service.Implementation;
using DAL.Db.Repositories.Implementation;
using WebApplication_Vy.Controllers;
using Moq;
using UTILS.Utils.Auth;

namespace WebApplication_Vy.Controllers.Tests
{
    [TestFixture]
    public class LoginControllerTests
    {
        [SetUp]
        public void setup()
        {
            var _LoginController = new LoginController(new LoginServiceImpl(new LoginRepositoryImpl(), new HashingAndSaltingService()))
            {
                
            }
            
            
        }
        
        
        [Test()]
        public void LoginControllerTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void LogoutTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void LoginTest()
        {
            Assert.Fail();
        }
    }
}