using System.Text.Json.Serialization;

namespace CryptoTracker_backend.Utils
{
    public class Roles
    {
        public const string Admin  = "Admin";
        public const string User  = "User";

    public static bool IsValidRole(string role)
    {
        return role == Admin || role  == User;
    }
    }
}
