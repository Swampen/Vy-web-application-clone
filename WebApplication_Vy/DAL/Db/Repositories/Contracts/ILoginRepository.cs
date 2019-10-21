﻿using MODEL.Models;

namespace DAL.Db.Repositories.Contracts
{
    public interface ILoginRepository
    {

        bool UserInDB(string username, string hashedPassword);

        bool DoseAdminUserExcist(AdminUser adminUser);

        bool RegisterAdminUser(AdminUser adminUser);
    }
}