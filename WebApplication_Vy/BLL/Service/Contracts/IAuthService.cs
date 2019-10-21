using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;

namespace BLL.Service.Contracts
{
    public interface IAuthService
    {
        string SecretKey { get; set; }

        bool isTokenValid(string token);

        //string GenerateToken(IAuthContainerModel model);

        IEnumerable<Claim> GetTokenClaims(string token);
    }
}