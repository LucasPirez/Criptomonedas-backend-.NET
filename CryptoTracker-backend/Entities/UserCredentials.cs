using CryptoTracker_backend.Utils;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CryptoTracker_backend.Entities
{
    public class UserCredentials
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
