using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using BLL.Service.Contracts;
using DAL.Db.Repositories.Contracts;
using DAL.Db.Repositories.Implementation;
using DAL.DTO;
using MODEL.Models;

namespace BLL.Service.Implementation
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes =
              new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }
            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }



        public bool Login(AdminUserDTO adminUserDto)
        {
            string salt = "somthing random";
            try
            {
                var hashedPassword = GenerateSaltedHash(Encoding.UTF8.GetBytes(adminUserDto.Password),
                        Encoding.UTF8.GetBytes(salt));

                AdminUser user = MapAdminUser(adminUserDto, hashedPassword);

                if (_loginRepository.UserInDB(user))
                {
                    return true;
                }
                return false;
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                return false;
            }

        }

        private AdminUser MapAdminUser(AdminUserDTO adminUserDto, byte[] hashedPassword)
        {
            var adminUser = new AdminUser();

            adminUser.UserName = adminUserDto.Username;
            adminUser.Password = hashedPassword;
            adminUser.SuperAdmin = adminUserDto.SuperAdmin;

            return adminUser;
        }


        public bool RegisterAdminUser(string Username, string Password, string SecretAdminPassword)
        {
            string salt = "somthing random";

            if (SecretAdminPassword.Equals("ADMINISTRATOR"))
            {
                AdminUser user = new AdminUser();
                user.Password = (GenerateSaltedHash(Encoding.UTF8.GetBytes(Password), Encoding.UTF8.GetBytes(salt)));
                user.UserName = Username;
                return _loginRepository.RegisterAdminUser(user);
            }

            return false;
        }
        public List<AdminUserDTO> GetAllAdmins()
        {
            var adminUserDtos = new List<AdminUserDTO>();
            _loginRepository
                .FindAllAdminUsers()
                .ForEach(adminUser => { adminUserDtos.Add(MapUserAdminDto(adminUser)); });
            return adminUserDtos;
        }
        private AdminUserDTO MapUserAdminDto(AdminUser admin)
        {
            return new AdminUserDTO
            {
                Id = admin.Id,
                Username = admin.UserName,
                SuperAdmin = admin.SuperAdmin
            };
        }

        public bool isSuperAdmin(string adminUsername)
        {
            return _loginRepository.isSuperAdmin(adminUsername);
        }

        public bool DeleteAdmin(int Id)
        {
            return _loginRepository.DeleteAdmin(Id);
        }
    }
}