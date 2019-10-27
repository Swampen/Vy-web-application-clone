using BLL.Service.Contracts;
using DAL.Db.Repositories.Contracts;
using DAL.DTO;
using MODEL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using UTILS.Utils.Auth;
using UTILS.Utils.Logging;

namespace BLL.Service.Implementation
{
    public class LoginServiceImpl : ILoginService
    {
        private static readonly ILog Log = LogHelper.GetLogger();
        private readonly ILoginRepository _loginRepository;
        private readonly HashingAndSaltingService _hashingAndSaltingService;
        public LoginServiceImpl(ILoginRepository loginRepository, HashingAndSaltingService hashingAndSaltingService)
        {
            _loginRepository = loginRepository;
            _hashingAndSaltingService = hashingAndSaltingService;
        }

        
        public bool Login(AdminUserDTO adminUserDto)
        {
            string salt = _loginRepository.getSalt(adminUserDto.Username);
            if (salt == "") return false;

            try
            {
                var hashedPassword = _hashingAndSaltingService.GenerateSaltedHash(Encoding.UTF8.GetBytes(adminUserDto.Password),
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
                Log.Error(LogEventPrefixes.AUTHENTICATION_ERROR + 
                          ": login error for user: " + adminUserDto.Username, error);
                return false;
            }

        }

        public AdminUser MapAdminUser(AdminUserDTO adminUserDto, byte[] hashedPassword)
        {
            var adminUser = new AdminUser();

            adminUser.UserName = adminUserDto.Username;
            adminUser.Password = hashedPassword;
            adminUser.SuperAdmin = adminUserDto.SuperAdmin;

            return adminUser;
        }

        public bool RegisterAdminUser(string Username, string Password)
        {
            string salt = _hashingAndSaltingService.MakeSalt();

            AdminUser user = new AdminUser();
            user.Password = (_hashingAndSaltingService.GenerateSaltedHash(Encoding.UTF8.GetBytes(Password), Encoding.UTF8.GetBytes(salt)));
            user.UserName = Username;
            user.salt = salt;
            return _loginRepository.RegisterAdminUser(user);
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