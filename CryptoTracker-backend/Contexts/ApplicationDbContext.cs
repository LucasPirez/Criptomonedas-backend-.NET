using CryptoTracker_backend.entities;
using CryptoTracker_backend.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CryptoTracker_backend.Contexts
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
        }


        public DbSet<Alert> Alerts  => Set<Alert>();
        public DbSet<UserCredentials> UsersCredentials => Set<UserCredentials>();
        public DbSet<User> Users  => Set<User>();
        
        public DbSet<CoinInAlert> CoinsInAlerts => Set<CoinInAlert>();
    }
}
