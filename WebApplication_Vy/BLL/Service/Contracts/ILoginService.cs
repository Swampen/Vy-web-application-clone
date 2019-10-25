using DAL.DTO;
using System.Collections.Generic;

namespace BLL.Service.Contracts
{
    public interface ILoginService
    {
        bool Login(AdminUserDTO adminUserDto);

        bool RegisterAdminUser(string Username, string Password, string SecretAdminPassword);

        bool DeleteAdmin(int Id);

        List<AdminUserDTO> GetAllAdmins();

        bool isSuperAdmin(string adminUsername);

    }
}