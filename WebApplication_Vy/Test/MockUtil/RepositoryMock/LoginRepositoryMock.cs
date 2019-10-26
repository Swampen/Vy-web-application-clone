using System.Collections.Generic;
using System.Text;
using BLL.Service.Contracts;
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
            mockRepo.Setup(mock => mock.UserInDB(It.IsAny<AdminUser>())).Returns(true);
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
            mockRepo.Setup(mock => mock.FindAllAdminUsers()).Returns(users);
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