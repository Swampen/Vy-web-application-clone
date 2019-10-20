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

        public bool UserInDB(string Username, string password)
        {
            using (var db = new VyDbContext())
            {
                
                var user = db.AdminUsers.FirstOrDefault(
                    db.AdminUsers,
                    b => b.Password == Password && b.UserName == UserName);
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


        public bool SuperUser(AdminUser adminUser)
        {
            return adminUser.SuperAdmin;
        }

        public bool DoseAdminUserExcist(adminUser adminUser)
        {
            using (var db = new VyDbContext())
            {
                var foundAdmin = Queryable.FirstOrDefault(db.AdminUsers,
                    user => user.UserName.Equals(adminUser.UserName))

                if (foundAdmin == null)
                {
                    return false
                }
                else
                {
                    return foundAdmin;
                }
            }
        }
        
        public bool RegisterUser()
    }
}