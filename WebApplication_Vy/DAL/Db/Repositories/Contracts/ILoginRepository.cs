using MODEL.Models;

namespace DAL.Db.Repositories.Contracts
{
    public interface ILoginRepository
    {
        bool getLoginConfirmation(string Username, string Password);

        bool UserInDB(string username, string hashedPassword);

        bool DoseAdminUserExcist(AdminUser adminUser);

        bool RegisterAdminUser(AdminUser adminUser);
    }
}