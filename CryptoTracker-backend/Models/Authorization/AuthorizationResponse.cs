using CryptoTracker_backend.Entities;

namespace CryptoTracker_backend.Models.Authorization
{
    public class AuthorizationResponse
    {

        public string Token { get; set; }

        public User userData { get; set; }

        public string Message { get; set; }
    }
}
