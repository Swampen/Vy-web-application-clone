﻿using System;
using System.Collections.Generic;
using BLL.Service.Contracts;
using DAL.DTO;
using Moq;

namespace Test.MockUtil.ServiceMock
{
    public class LoginServiceMock
    {
        private static readonly List<AdminUserDTO> _adminUserDtos = new List<AdminUserDTO>
        {
            new AdminUserDTO{Id = 1, Password = "test", Username = "test", SuperAdmin = true},
            new AdminUserDTO{Id = 2, Password = "test1", Username = "test1", SuperAdmin = false},
            new AdminUserDTO{Id = 3, Password = "test2", Username = "test2", SuperAdmin = false},
            new AdminUserDTO{Id = 4, Password = "test3", Username = "test3", SuperAdmin = false},
        };
        
        public static ILoginService GetAllAdminsMock()
        {
            var mockService = new Mock<ILoginService>();
            mockService.Setup(mock => mock.GetAllAdmins()).Returns(_adminUserDtos);
            return mockService.Object;
        }

        public static ILoginService LoginMock()
        {
            var mockService = new Mock<ILoginService>();
            mockService.Setup(mock => mock.Login(It.IsAny<AdminUserDTO>())).Returns(true);
            return mockService.Object;
        }

        public static ILoginService RegisterAdminUserMock()
        {
            var mockService = new Mock<ILoginService>();
            mockService.Setup(mock => mock.RegisterAdminUser(
                    "true",
                    "true",
                    "ADMINISTRATOR"))
                .Returns(true);
            mockService.Setup(mock => mock.RegisterAdminUser(
                    "false",
                    "false",
                    "false"))
                .Returns(false);
            return mockService.Object;
        }

        public static ILoginService IsSuperAdminMock()
        {
            var mockService = new Mock<ILoginService>();
            mockService.Setup(mock => mock.isSuperAdmin("true")).Returns(true);
            mockService.Setup(mock => mock.isSuperAdmin("false")).Returns(false);
            return mockService.Object;
        }

        public static ILoginService DeleteAdminMock()
        {
            var mockService = new Mock<ILoginService>();
            mockService.Setup(mock => mock.DeleteAdmin(1)).Returns(true);
            mockService.Setup(mock => mock.DeleteAdmin(2)).Returns(false);
            return mockService.Object;
        }
    }
}