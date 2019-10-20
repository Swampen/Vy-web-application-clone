using System;
using System.Security.Cryptography;
using BLL.Service.Contracts;
using DAL.Db.Repositories.Contracts;
using DAL.Db.Repositories.Implementation;
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
        
        static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
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

        
        
        public bool Login(string Username, string Password)
        {
            string salt = "somthing random"
            adminUser.Password = GenerateSaltedHash(Encoding.UTF8.GetBytes(Password),Encoding.UTF8.GetBytes(salt))
            if (_loginRepository.UserInDB(Username, Password))
                return true;
            else
            {
                return false;
            }
        }

        public bool RegisterUser(string Username, string Password, string SecretAdminPassword)
        {
            if (SecretAdminPassword.Equals("ADMINISTRATOR"))
            {
                
            }
        }
    }
}