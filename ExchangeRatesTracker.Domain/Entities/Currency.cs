namespace ExchangeRatesTracker.Domain.Entities
{
    public class Currency
    { 
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
        public List<ExchangeRate> ExchangeRates { get; set; } = new List<ExchangeRate>();
    }
}
