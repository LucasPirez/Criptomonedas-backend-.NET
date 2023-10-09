using CryptoTracker_backend.entities;
using CryptoTracker_backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoTracker_backend.Contexts
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<UserCredentials> UsersCredentials { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
