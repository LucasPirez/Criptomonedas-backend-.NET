using CryptoTracker_backend.Entities;
using System.Security.Claims;

namespace CryptoTracker_backend.Services
{
    public interface ITokenService
    {
       public string CreateToken(UserCredentials userInfo);
       public bool IsUserToken(int numUser, ClaimsPrincipal UserClaim);
    }
}
