using System;
using System.Linq;
using DAL.Db.Repositories.Contracts;
using DAL.DTO;
using MODEL.Models;
using MODEL.Models.Entities;

namespace DAL.Db.Repositories.Implementation
{
    public class LoginRepositoryImpl : ILoginRepository
    {

        public bool UserInDB(string username, string password)
        {
            using (var db = new VyDbContext())
            {
                
                var user = Queryable.FirstOrDefault(
                    db.AdminUsers,
                    admin => admin.UserName.Equals(username) && admin.Password.Equals(password));
                if (user == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
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
                var foundAdmin = Queryable.FirstOrDefault(db.AdminUsers,
                    user => user.UserName.Equals(adminUser.UserName));

                if (foundAdmin == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public bool RegisterUser(AdminUser adminUser)
        {
            using (var db = new VyDbContext())
            {
                var userAlreadyExcist = Queryable.FirstOrDefault(db.AdminUsers,
                    admin => admin.UserName.Equals(adminUser.UserName));

                if (userAlreadyExcist != null)
                {
                    return false;
                }
                else
                {
                    var user = new AdminUser
                    {
                        UserName = adminUser.UserName,
                        Id = adminUser.Id,
                        Password = adminUser.Password,
                    };
                    try
                    {
                        db.AdminUsers.Add(user);
                        db.SaveChanges();
                        Console.WriteLine("User with username: " + adminUser.UserName);
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }
        }
    }
}