using ExchangeRatesTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExchangeRatesTracker.Infrastructure.Persistence.EntityConfigs
{
    internal class ExchangeRateEntityConfiguration : IEntityTypeConfiguration<ExchangeRate>
    {
        public void Configure(EntityTypeBuilder<ExchangeRate> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Rate).HasColumnType("decimal(18,9)").IsRequired();
            builder.HasOne(x => x.Сurrency).WithMany(x => x.ExchangeRates).HasForeignKey(x => x.СurrencyCode);
            builder.HasIndex(nameof(ExchangeRate.СurrencyCode), nameof(ExchangeRate.Date));
        }
    }
}
