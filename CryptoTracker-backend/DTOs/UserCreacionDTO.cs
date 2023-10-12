using CryptoTracker_backend.Utils;

namespace CryptoTracker_backend.DTOs
{
    public class UserCreacionDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Roles { get; set; } = string.Empty;

    }
}
