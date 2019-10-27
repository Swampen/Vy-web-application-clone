using System;
using System.Collections.Generic;
using System.Text;
using BLL.Service.Contracts;
using BLL.Service.Implementation;
using DAL.DTO;
using MODEL.Models.Entities;
using NUnit.Framework;
using Test.MockUtil.RepositoryMock;
using UTILS.Utils.Auth;

namespace BLL.Service.Tests
{
    [TestFixture]
    public class LoginServiceImplTests
    {
        [SetUp]
        [TearDown]
        public void TearDown()
        {
            _adminUserDto = null;
        }

        private HashingAndSaltingService _hashingAndSaltingService;

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
        [TestCase(1)]
        [TestCase(99)]
        public void DeleteAdmin_shouldReturnTrue(int id)
        {
            var service = new LoginServiceImpl(LoginRepositoryMock.DeleteAdmin(), _hashingAndSaltingService);

            var actual = service.DeleteAdmin(id);

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
        public void Login_emptySalt_shouldReturnFalse()
        {
            //Arrange
            var service = new LoginServiceImpl(LoginRepositoryMock.getSalt(), null);

            //Act
            var actual = service.Login(testUser);

            //Assert
            Assert.IsFalse(actual);
        }

        [Test]
        [TestCase("true")]
        [TestCase("false")]
        public void Login_IfUsernameIsCorrect(string value)
        {
            //Arrange
            var service = new LoginServiceImpl(
                LoginRepositoryMock.UserInDB(),
                new HashingAndSaltingService());
            testUser.Username = value;

            //Act
            var actual = service.Login(testUser);

            //Assert
            if (value.Equals("true"))
                Assert.True(actual);
            else
                Assert.False(actual);
        }

        [Test]
        public void Login_exceptionReturnsFalse()
        {
            //Arrange
            var service = new LoginServiceImpl(
                LoginRepositoryMock.UserInDB(),
                new HashingAndSaltingService());
            testUser.Username = "error";
            
            //Act 
            var actual = service.Login(testUser);
            
            //Assert
            Assert.False(actual);

                

        }

        [Test]
        public void LoginServiceIfUsernameIsFalse()
        {
            var service = new LoginServiceImpl(LoginRepositoryMock.UserInDB(), new HashingAndSaltingService());

            var actual = service.Login(new AdminUserDTO
            {
                Id = 1, Password = "test", SuperAdmin = true, Username = "test"
            });

            Assert.AreEqual(false, actual);
        }

        [Test]
        public void MapAdminUser_shouldReturnAdminUser()
        {
            var service = new LoginServiceImpl(LoginRepositoryMock.DeleteAdmin(), _hashingAndSaltingService);

            var actual = service.MapAdminUser(testUser, Encoding.ASCII.GetBytes(testUser.Password));

            Assert.IsInstanceOf(typeof(AdminUser), actual);
        }

        [Test]
        [TestCase("admin", "admin")]
        [TestCase("null", "null")]
        public void RegisterAdminUserTest(string name, string password)
        {
            var service = new LoginServiceImpl(LoginRepositoryMock.RegisterAdminUser(), new HashingAndSaltingService());


            var actual = service.RegisterAdminUser(name, password);

            if (name == null || password == null) Assert.IsFalse(actual);

            Assert.IsTrue(actual);
        }

        [Test]
        public void DeleteAdminTest_shouldReturnBool()
        {
            //Arrange
            var service = new LoginServiceImpl(LoginRepositoryMock.DeleteAdmin(), null);
            
            //Act
            var actual = service.DeleteAdmin(1);
            
            //Assert
            Assert.True(actual);
        }
    }
}