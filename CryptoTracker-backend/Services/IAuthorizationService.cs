
using CryptoTracker_backend.Models.Authorization;

namespace CryptoTracker_backend.Services
{
    public interface IAuthorizationService
    {

        Task<AuthorizationResponse> DevolverToken(AuthorizationResponse authorization);
    }
}
