using System;
using System.Security.Cryptography;
using log4net;
using UTILS.Utils.Logging;

namespace UTILS.Utils.Auth
{
    public class HashingAndSaltingService
    {
        private static readonly ILog Log = LogHelper.GetLogger();

        public byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            Log.Info("Generating salted hash");
            HashAlgorithm algorithm = new SHA256Managed();

            var plainTextWithSaltBytes =
                new byte[plainText.Length + salt.Length];

            for (var i = 0; i < plainText.Length; i++) plainTextWithSaltBytes[i] = plainText[i];
            for (var i = 0; i < salt.Length; i++) plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }

        public string MakeSalt()
        {
            Log.Info("Generating random salt");
            var randomArray = new byte[10];
            string randomString;

            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomArray);
            randomString = Convert.ToBase64String(randomArray);

            return randomString;
        }
    }
}