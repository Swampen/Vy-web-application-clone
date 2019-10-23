using System.Collections.Generic;
using DAL.DTO;
using MODEL.Models;

namespace BLL.Service.Contracts
{
    public interface ILoginService
    {
        bool Login(AdminUserDTO adminUserDto);

        byte[] GenerateSaltedHash(byte[] plainText, byte[] salt);
        
        bool RegisterAdminUser(string Username, string Password, string SecretAdminPassword);

        List<AdminUserDTO> GetAllAdmins();

        bool isSuperAdmin(string adminUsername);
    }
}    