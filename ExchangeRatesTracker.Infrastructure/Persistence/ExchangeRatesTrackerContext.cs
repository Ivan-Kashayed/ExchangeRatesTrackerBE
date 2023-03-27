using ExchangeRatesTracker.App.Interfaces;
using ExchangeRatesTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRatesTracker.Infrastructure.Persistence
{
    internal class ExchangeRatesTrackerContext : DbContext, IExchangeRatesTrackerContext
    {
        public ExchangeRatesTrackerContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExchangeRatesTrackerContext).Assembly);
        }
    }
}
