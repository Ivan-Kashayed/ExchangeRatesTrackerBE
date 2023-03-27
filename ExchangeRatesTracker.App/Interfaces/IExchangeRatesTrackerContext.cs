using ExchangeRatesTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ExchangeRatesTracker.App.Interfaces
{
    public interface IExchangeRatesTrackerContext
    {
        DbSet<Currency> Currencies { get; set; }
        DbSet<ExchangeRate> ExchangeRates { get; set; }

        Task<int> SaveChangesAsync(CancellationToken token = default);
        int SaveChanges();
        public DatabaseFacade Database { get; }
    }
}