using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Db.Repositories.Contracts;
using log4net;
using MODEL.Models;
using MODEL.Models.Entities;
using UTILS.Utils.Logging;

namespace DAL.Db.Repositories.Implementation
{
    public class LoginRepositoryImpl : ILoginRepository
    {
        private static readonly ILog Log = LogHelper.GetLogger();

        public bool UserInDB(string username, string hashedPassword)
        {
            using (var db = new VyDbContext())
            {
                var user = db.AdminUsers.FirstOrDefault(admin => admin.UserName.Equals(username) && admin.Password.Equals(hashedPassword));
                
                return user != null;
            }
        }


        public bool DoseAdminUserExcist(AdminUser adminUser)
        {
            using (var db = new VyDbContext())
            {
                var foundAdmin = db.AdminUsers.FirstOrDefault(user => user.UserName.Equals(adminUser.UserName));

                return foundAdmin != null;
            }
        }

        public bool RegisterAdminUser(AdminUser adminUser)
        { 
            var user = new AdminUser
            {
                UserName = adminUser.UserName,
                Password = adminUser.Password
            };
            using (var db = new VyDbContext())
            {
                var excistingAdmin = db.AdminUsers.FirstOrDefault(admin => admin.UserName.Equals(adminUser.UserName));

                if (excistingAdmin != null)
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
                    Log.Error(LogEventPrefixes.DATABASE_ERROR + error.Message, error);
                    return false;
                }
            }
        }
        public List<AdminUser> FindAllAdminUsers()
        {
            using (var db = new VyDbContext())
            {
                try
                {
                    List<AdminUser> admin = db.AdminUsers.ToList();
                    return admin.OrderBy(s => s.UserName).ToList();
                }
                catch (Exception e)
                {
                    Log.Error(LogEventPrefixes.DATABASE_ERROR + e.Message, e);
                    throw;
                }
            }
        }
    }
    }
}