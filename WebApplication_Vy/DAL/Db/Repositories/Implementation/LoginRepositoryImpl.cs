using DAL.Db.Repositories.Contracts;
using log4net;
using MODEL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using UTILS.Utils.Logging;

namespace DAL.Db.Repositories.Implementation
{
    public class LoginRepositoryImpl : ILoginRepository
    {
        private static readonly ILog Log = LogHelper.GetLogger();

        public bool UserInDB(AdminUser inAdminUser)
        {
            using (var db = new VyDbContext())
            {
                var query = db.AdminUsers.FirstOrDefault(admin => admin.UserName == inAdminUser.UserName &&
                            admin.Password == inAdminUser.Password);


                Console.WriteLine(query);
                if (query != null)
                {
                    Log.Info(LogEventPrefixes.DATABASE_ACCESS +
                             ": found adminUser: " + inAdminUser.UserName + " in database");
                    return true;
                }
            }
            Log.Info(LogEventPrefixes.DATABASE_ACCESS +
                     ": could not find adminUser: " + inAdminUser.UserName + " in database");
            return false;
        }

        public string getSalt(string username)
        {
            using (var db = new VyDbContext())
            {
                var User = db.AdminUsers.FirstOrDefault(user => user.UserName.Equals(username));

                if (User != null)
                {
                    Log.Info(LogEventPrefixes.DATABASE_ACCESS +
                             ": fetched salt for adminUser: " + username + " in database");
                    return User.salt;
                }

                Log.Info(LogEventPrefixes.DATABASE_ACCESS +
                         ": could not find salt for adminUser: " + username + " in database");
                return "";
            }
        }

        public bool RegisterAdminUser(AdminUser adminUser)
        {

            using (var db = new VyDbContext())
            {
                var excistingAdmin = db.AdminUsers.FirstOrDefault(admin => admin.UserName.Equals(adminUser.UserName));

                if (excistingAdmin != null)
                {
                    Log.Error(LogEventPrefixes.DATABASE_ERROR + ": there is an existing entry for username: " +
                              adminUser.UserName + "registered in the database");
                    return false;
                }

                try
                {
                    db.AdminUsers.Add(adminUser);
                    db.SaveChanges();
                    Log.Info(LogEventPrefixes.DATABASE_ACCESS +
                                 "Create admin succeded for username: " + adminUser.UserName);
                    return true;
                }
                catch (Exception error)
                {
                    Log.Error(LogEventPrefixes.DATABASE_ERROR + ": " + error.Message, error);
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
                    List<AdminUser> admins = db.AdminUsers.ToList();

                    Log.Info(LogEventPrefixes.DATABASE_ACCESS + ": fetching all adminUsers from database");
                    return admins;
                }
                catch (Exception e)
                {
                    Log.Error(LogEventPrefixes.DATABASE_ERROR + ": " + e.Message, e);
                    return null;
                }
            }
        }

        public bool isSuperAdmin(string adminUsername)
        {
            var db = new VyDbContext();

            AdminUser admin = db.AdminUsers.FirstOrDefault(a => a.UserName == adminUsername);

            if (admin != null)
            {
                Log.Info(LogEventPrefixes.DATABASE_ACCESS + ": found match for superadmin: " +
                         adminUsername + "in database");
                return admin.SuperAdmin;
            }
            Log.Warn(LogEventPrefixes.DATABASE_ACCESS + ": could not find match for superadmin: " +
                     adminUsername + "in database");
            return false;
        }

        public bool DeleteAdmin(int Id)
        {
            var db = new VyDbContext();
            var admin = db.AdminUsers.Find(Id);

            if (admin != null)
            {
                try
                {
                    db.AdminUsers.Remove(admin);
                    db.SaveChanges();
                    Log.Info(LogEventPrefixes.DATABASE_ACCESS +
                                 ": Delete admin succeded for Id: " + Id);
                    return true;

                }
                catch (Exception e)
                {
                    Log.Error(LogEventPrefixes.DATABASE_ERROR + ": delete admin failed, " +  e.Message, e);
                    return false;
                }
            }
            Log.Warn(LogEventPrefixes.DATABASE_ACCESS + ": could not delete adminuser:" + admin.UserName + 
                     " no matching entry found in database");
            return false;
        }
    }
}

