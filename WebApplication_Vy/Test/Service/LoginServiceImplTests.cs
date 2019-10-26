using NUnit.Framework;
using BLL.Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Service.Contracts;
using DAL.DTO;
using Test.MockUtil;
using Test.MockUtil.RepositoryMock;

using UTILS.Utils.Auth;

namespace BLL.Service.Tests
{
    [TestFixture]
    public class LoginServiceImplTests
    {
        private HashingAndSaltingService _hashingAndSaltingService;
        [SetUp]
        public void SetUp()
        {
           var _adminDto = new AdminUserDTO
            {
                Id = 1,
                Password = "test",
                SuperAdmin = true,
                Username = "test"
            };

        }
        [TearDown]
        public void TearDown()
        {
            _adminUserDto = null;
        }

        private ILoginService _service;
        private AdminUserDTO _adminUserDto;
        
        [Test]
        public void LoginServiceIfUsernameIsCorrect()
        {
           
           

            
            Assert.AreEqual("True",actual);
        }
        
        [Test]
        public void LoginTest()
        {
            Assert.Fail();
        }

        [Test]
        public void RegisterAdminUserTest()
        {
            Assert.Fail();
        }

        [Test]
        public void GetAllAdminsTest()
        {
            Assert.Fail();
        }

        [Test]
        public void isSuperAdminTest()
        {
            Assert.Fail();
        }
    }
}