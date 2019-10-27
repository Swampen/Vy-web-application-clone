using NUnit.Framework;
using BLL.Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Service.Contracts;
using DAL.DTO;
using MODEL.Models.Entities;
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
        
        [TearDown]
        public void TearDown()
        {
            _adminUserDto = null;
        }

        private ILoginService _service;
        private AdminUserDTO _adminUserDto;

        private readonly AdminUserDTO testUser = new AdminUserDTO
        {
            Id = 1,
            Password = "admin",
            SuperAdmin = true,
            Username = "admin"
        };
        
        [Test]
        public void LoginService_IfUsernameIsCorrect()
        {

            
            var service = new LoginServiceImpl(LoginRepositoryMock.UserInDB(), new HashingAndSaltingService());
           
           var actual = service.Login(testUser);
           
           Assert.AreEqual(true,actual);
           
        }
        
        [Test]
         public void LoginServiceIfUsernameIsFalse()
        {
           var service = new LoginServiceImpl(LoginRepositoryMock.UserInDB(), new HashingAndSaltingService());
           
           var actual = service.Login(new AdminUserDTO
           {
               Id = 1, Password = "test", SuperAdmin = true, Username = "test"
           });
           
           Assert.AreEqual(false,actual);
        }

         [Test]
        [TestCase("admin", "admin")]
        [TestCase("null", "null")]
        public void RegisterAdminUserTest(string name, string password)
        {
            var service = new LoginServiceImpl(LoginRepositoryMock.RegisterAdminUser(), new HashingAndSaltingService());

           
            var actual = service.RegisterAdminUser(name, password);
            
            if (name == null || password == null)
            {
                Assert.IsFalse(actual);
            }
            
            Assert.IsTrue(actual);
        }

        [Test]
        public void GetAllAdmins_shouldReturnList()
        {
            var service = new LoginServiceImpl(LoginRepositoryMock.FindAllAdminUsers(), _hashingAndSaltingService);

            var actual = service.GetAllAdmins();

            Assert.IsInstanceOf<List<AdminUserDTO>>(actual);

            Assert.AreEqual(3, actual.Count);
            foreach (var user in actual)
            {
                Assert.IsInstanceOf<AdminUserDTO>(user);
                Assert.IsNotNull(user.Username);
            }
        }

        [Test]
        [TestCase("admin")]
        public void isSuperAdmin_shouldReturnTrue(string name)
        {
            var service = new LoginServiceImpl(LoginRepositoryMock.isSuperAdmin(), _hashingAndSaltingService);

            var actual = service.isSuperAdmin(name);

            Assert.AreEqual(true, actual);
        }

        [Test]
        [TestCase(1)]
        [TestCase(99)]
        public void DeleteAdmin_shouldReturnTrue(int id)
        {
            var service = new LoginServiceImpl(LoginRepositoryMock.DeleteAdmin(), _hashingAndSaltingService);

            var actual = service.DeleteAdmin(id);
            
            Assert.IsTrue(actual);
        }

        [Test]
        public void MapAdminUser_shouldReturnAdminUser()
        {
            var service = new LoginServiceImpl(LoginRepositoryMock.DeleteAdmin(), _hashingAndSaltingService);

            var actual = service.MapAdminUser(testUser,Encoding.ASCII.GetBytes(testUser.Password));
            
            Assert.IsInstanceOf(typeof(AdminUser), actual);
        }
    }
}