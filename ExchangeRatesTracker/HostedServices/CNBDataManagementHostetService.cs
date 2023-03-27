using ExchangeRatesTracker.App.DataManagement.CzechNationalBank;

namespace ExchangeRatesTracker.API.HostedServices
{
    public class CNBDataManagementHostetService : IHostedService, IDisposable
    {
        private readonly ILogger<CNBDataManagementHostetService> _logger;
        private readonly CzechNationalBankService _czechNationalBankService;
        private Timer _timer;

        private const int _rateDeclareHourUtc = 13;
        private const int _rateDeclareMinuteUtc = 30;

        private readonly int _rateDeclareTimeInMinutesUTC = _rateDeclareHourUtc * 60 + _rateDeclareMinuteUtc;

        public CNBDataManagementHostetService(ILogger<CNBDataManagementHostetService> logger,
                                                   CzechNationalBankService czechNationalBankService)
        {
            _logger = logger;
            _czechNationalBankService = czechNationalBankService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Timed {nameof(CNBDataManagementHostetService)} running.");

            await _czechNationalBankService.SyncAsync(cancellationToken);
            var nextSyncTimeSpan = GetNextSyncTimeSpan();

            _timer = new Timer(
                async _ => await _czechNationalBankService.SyncAsync(cancellationToken), 
                null,
                nextSyncTimeSpan,
                TimeSpan.FromDays(1));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Timed {nameof(CNBDataManagementHostetService)} is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private TimeSpan GetNextSyncTimeSpan()
        {
            var utcDate = DateTime.UtcNow;
            if ((utcDate.Hour * 60 + utcDate.Minute) > _rateDeclareTimeInMinutesUTC)
            {
                utcDate = utcDate.AddDays(1);
            }

            var nextSyncTime = new DateTime(
                utcDate.Year,
                utcDate.Month,
                utcDate.Day,
                _rateDeclareHourUtc,
                _rateDeclareMinuteUtc,
                0,
                DateTimeKind.Utc);

            return nextSyncTime - DateTime.UtcNow;
        }
    }
}
