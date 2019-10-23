namespace BLL.Service.Contracts
{
    public interface ILoginService
    {
        bool Login(string Username, string Password);

        byte[] GenerateSaltedHash(byte[] plainText, byte[] salt);
        
        bool RegisterAdminUser(string Username, string Password, string SecretAdminPassword);
    }
}    