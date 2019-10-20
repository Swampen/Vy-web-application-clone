using MODEL.Models;

namespace DAL.Db.Repositories.Contracts
{
    public interface ILoginRepository
    {
        bool getLoginConfirmation(string Username, string Password);

        bool getAdminStatus(AdminUser adminUser);

        bool SuperUser(AdminUser adminUser);

        bool UserInDB(AdminUser adminUser);
        
    }
}