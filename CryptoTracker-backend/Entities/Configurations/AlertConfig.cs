using CryptoTracker_backend.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoTracker_backend.Entities.Configurations
{
    public class AlertConfig : IEntityTypeConfiguration<Alert>

    {
        public void Configure(EntityTypeBuilder<Alert> builder)
        {
            builder.HasIndex(x => new { x.UserId, x.CoinName }).IsUnique();
        }
    }
}
