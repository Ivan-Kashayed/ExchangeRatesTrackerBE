using ExchangeRatesTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExchangeRatesTracker.Infrastructure.Persistence.EntityConfigs
{
    internal class CurrencyEntityConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(x => x.Code);
            builder.Property(x => x.Code).HasMaxLength(3);
            builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
            builder.HasMany(x => x.ExchangeRates).WithOne(x => x.Сurrency).HasForeignKey(x => x.СurrencyCode);
        }
    }
}
