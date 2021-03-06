﻿using System;
using System.Collections.Generic;
using System.Text;
using DAL.Db.Repositories.Contracts;
using MODEL.Models.Entities;
using Moq;

namespace Test.MockUtil.RepositoryMock
{
    public class LoginRepositoryMock
    {
        public static AdminUser user = new AdminUser
        {
            Id = 1,
            UserName = "test",
            Password = Encoding.ASCII.GetBytes("test"),
            salt = "test",
            SuperAdmin = true
        };

        private static readonly byte[] pWord = Encoding.ASCII.GetBytes("admin");

        private static readonly List<AdminUser> _users = new List<AdminUser>
        {
            new AdminUser {Id = 1, Password = pWord, salt = "salt", SuperAdmin = true, UserName = "admin"},
            new AdminUser {Id = 2, Password = pWord, salt = "salt", SuperAdmin = true, UserName = "1234"},
            new AdminUser {Id = 3, Password = pWord, salt = "salt", SuperAdmin = true, UserName = "mhm"}
        };

        public static List<AdminUser> users = new List<AdminUser>();

        public static ILoginRepository CreateUserMock()
        {
            var mockRepo = new Mock<ILoginRepository>();
            mockRepo.Setup(mock => mock.RegisterAdminUser(It.Is<AdminUser>(user => user.UserName.Equals("false"))))
                .Returns(false);
            mockRepo.Setup(mock =>
                mock.RegisterAdminUser(It.Is<AdminUser>(adminUser => adminUser.UserName.Equals("True")))).Returns(true);
            return mockRepo.Object;
        }

        public static ILoginRepository UserInDB()
        {
            var mockRepo = new Mock<ILoginRepository>();
            mockRepo.Setup(mock => mock.UserInDB(It.Is<AdminUser>(adminUser =>
                adminUser.UserName.Equals("true")))).Returns(true);
            mockRepo.Setup(mock =>
                mock.UserInDB(It.Is<AdminUser>(adminUser => adminUser.UserName.Equals("false")))).Returns(false);
            mockRepo.Setup(mock =>
                mock.getSalt(It.IsAny<string>())).Returns("salt");
            mockRepo.Setup(mock =>
                    mock.UserInDB(It.Is<AdminUser>(adminUser => adminUser.UserName.Equals("error"))))
                .Throws(new Exception());
            return mockRepo.Object;
        }

        public static ILoginRepository getSalt()
        {
            var mockRepo = new Mock<ILoginRepository>();
            mockRepo.Setup(mock => mock.getSalt(It.IsAny<string>())).Returns("");
            return mockRepo.Object;
        }

        public static ILoginRepository RegisterAdminUser()
        {
            var mockRepo = new Mock<ILoginRepository>();
            mockRepo.Setup(mock => mock.RegisterAdminUser(It.IsAny<AdminUser>())).Returns(true);
            return mockRepo.Object;
        }

        public static ILoginRepository FindAllAdminUsers()
        {
            var mockRepo = new Mock<ILoginRepository>();
            users.Add(user);
            mockRepo.Setup(mock => mock.FindAllAdminUsers()).Returns(_users);
            return mockRepo.Object;
        }

        public static ILoginRepository isSuperAdmin()
        {
            var mockRepo = new Mock<ILoginRepository>();
            mockRepo.Setup(mock => mock.isSuperAdmin(It.IsAny<string>())).Returns(true);
            return mockRepo.Object;
        }

        public static ILoginRepository DeleteAdmin()
        {
            var mockRepo = new Mock<ILoginRepository>();
            mockRepo.Setup(mock => mock.DeleteAdmin(It.IsAny<int>())).Returns(true);
            return mockRepo.Object;
        }
    }
}