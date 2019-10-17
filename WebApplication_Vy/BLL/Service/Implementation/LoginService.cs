using System;

namespace BLL.Service.Implementation
{
    public class LoginService
    {
        public string ComputeHash(String Password)
        {
            var algoritme = System.Security.Cryptography.SHA512.Create();
            byte[] inndata;
            byte[] utdata; 
            inndata = System.Text.Encoding.ASCII.GetBytes(Password); 
            utdata = algoritme.ComputeHash(inndata);
            var HashedPword = utdata.ToString();
            
            return HashedPword;
        }
    }
}