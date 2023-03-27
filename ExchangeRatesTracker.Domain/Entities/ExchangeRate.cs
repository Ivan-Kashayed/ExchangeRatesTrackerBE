namespace ExchangeRatesTracker.Domain.Entities
{
    public class ExchangeRate
    {
        public Guid Id { get; set; }
        public string СurrencyCode { get; set; }
        public Currency Сurrency { get; set; }
        public DateTime Date { get; set; }
        public decimal Rate { get; set; }
    }
}
