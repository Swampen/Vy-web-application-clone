using System.Security.Claims;

namespace MODEL.Models.JwtModels
{
    public interface IAuthContainerModel
    {
        #region Members

        string SecretKey { get; set; }
        string SecurityAlgorithm { get; set; }
        int ExpiresMinutes { get; set; }
        
        Claim[] Claims { get; set; }
        #endregion
    }
}