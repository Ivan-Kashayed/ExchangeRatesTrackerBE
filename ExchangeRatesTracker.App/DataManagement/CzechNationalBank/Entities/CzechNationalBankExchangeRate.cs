namespace ExchangeRatesTracker.App.DataManagement.CzechNationalBank.Entities
{
    public class CzechNationalBankExchangeRate
    {
        public CzechNationalBankCurrency CzechNationalBankCurrency  { get; set; }
        public DateTime Date { get; set; }
        public decimal Rate { get; set; }
    }
}
