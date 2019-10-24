using MODEL.Models;
using System.Collections.Generic;
using MODEL.Models.Entities;

namespace DAL.Db.Repositories.Contracts
{
    public interface ILoginRepository
    {

        bool UserInDB(AdminUser adminUser);

        bool DoseAdminUserExcist(AdminUser adminUser);

        bool RegisterAdminUser(AdminUser adminUser);

        bool DeleteAdmin(int Id);

        List<AdminUser> FindAllAdminUsers();

        bool isSuperAdmin(string adminUsername);

        string getSalt(string username);
    }
}