namespace ExchangeRatesTracker.App.DTOs
{
    public class ExchangeRateDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Rate { get; set; }
    }
}
