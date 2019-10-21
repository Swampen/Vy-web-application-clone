using System;
using System.Linq;
using DAL.Db.Repositories.Contracts;
using MODEL.Models;

namespace DAL.Db.Repositories.Implementation
{
    public class LoginRepositoryImpl : ILoginRepository
    {

        public bool UserInDB(string username, string password)
        {
            using (var db = new VyDbContext())
            {
                var user = db.AdminUsers.FirstOrDefault(admin => admin.UserName.Equals(username) && admin.Password.Equals(password));
                
                return user != null;
            }
        }


        public bool getLoginConfirmation(string Username, string Password)
        {
            throw new NotImplementedException();
        }
        

        public bool DoseAdminUserExcist(AdminUser adminUser)
        {
            using (var db = new VyDbContext())
            {
                var foundAdmin = db.AdminUsers.FirstOrDefault(user => user.UserName.Equals(adminUser.UserName));

                return foundAdmin != null;
            }
        }

        public bool RegisterUser(AdminUser adminUser)
        { 
            var user = new AdminUser
            {
                UserName = adminUser.UserName,
                Id = adminUser.Id,
                Password = adminUser.Password
                
            };
            using (var db = new VyDbContext())
            {
                var userAlreadyExcist = db.AdminUsers.FirstOrDefault(admin => admin.UserName.Equals(adminUser.UserName));

                if (userAlreadyExcist != null)
                {
                    return false;
                }
                
                try
                {
                    db.AdminUsers.Add(user);
                    db.SaveChanges();
                    Console.WriteLine("User with username: " + adminUser.UserName);
                    return true;
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                    Console.WriteLine(error.StackTrace);
                    return false;
                }
            }
        }
    }
}