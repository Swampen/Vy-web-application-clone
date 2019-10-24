using MODEL.Models;
using System.Collections.Generic;

namespace DAL.Db.Repositories.Contracts
{
    public interface ILoginRepository
    {

        bool UserInDB(AdminUser adminUser);

        bool DoseAdminUserExcist(AdminUser adminUser);

        bool RegisterAdminUser(AdminUser adminUser);
        List<AdminUser> FindAllAdminUsers();

        bool isSuperAdmin(string adminUsername);

        string getSalt(string username);
    }
}