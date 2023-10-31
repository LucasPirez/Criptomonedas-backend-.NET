using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoTracker_backend.Entities.Configurations
{
    public class CoinInAlertConfig : IEntityTypeConfiguration<CoinInAlert>
    {
        public void Configure(EntityTypeBuilder<CoinInAlert> builder)
        {
            builder.HasIndex(a=> a.CoinId).IsUnique();
        }
    }
}
