using System;
using System.Security.Cryptography;
using System.Text;
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



        public bool Login(string Username, string Password)
        {
            string salt = "somthing random";
            var hashedPassword = GenerateSaltedHash(Encoding.UTF8.GetBytes(Password), Encoding.UTF8.GetBytes(salt))
                .ToString();
            if (_loginRepository.UserInDB(Username, hashedPassword)) 
                return true;
            return false;
        }
        

        public bool RegisterAdminUser(string Username, string Password, string SecretAdminPassword)
        {
            string salt = "somthing random";
                
            if (SecretAdminPassword.Equals("ADMINISTRATOR"))
            {
                AdminUser user = new AdminUser();
                user.Password = (GenerateSaltedHash(Encoding.UTF8.GetBytes(Password), Encoding.UTF8.GetBytes(salt)))
                    .ToString();
                user.UserName = Username;
                Console.WriteLine(user.ToString());
                try
                {
                    _loginRepository.RegisterAdminUser(user);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return false;
        }
    }
}