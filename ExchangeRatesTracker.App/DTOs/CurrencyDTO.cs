namespace ExchangeRatesTracker.App.DTOs
{
    public class CurrencyDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
    }
}
