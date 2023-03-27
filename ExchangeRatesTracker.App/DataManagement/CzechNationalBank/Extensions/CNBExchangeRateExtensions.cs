using ExchangeRatesTracker.App.DataManagement.CzechNationalBank.Entities;
using System.Globalization;

namespace ExchangeRatesTracker.App.DataManagement.CzechNationalBank.Extensions
{
    public static class CNBExchangeRateExtensions
    {
        public static IEnumerable<CzechNationalBankExchangeRate> ParseResponse(this string response)
        {
            var allExchangeRates = new List<CzechNationalBankExchangeRate>();

            response = response.Replace("\n", "|\n|");
            var records = response.Split('|', ' ').Where(s => s != "").ToArray();

            var currencies = new List<CzechNationalBankCurrency>();
            var exchangeRates = new List<CzechNationalBankExchangeRate>();

            var isHeader = true;
            DateTime? date = null;
            int columnIndex = 0;

            for (int i = 0; i < records.Length; i++)
            {
                if (records[i] == "Date")
                {
                    currencies = new List<CzechNationalBankCurrency>();
                    isHeader = true;
                    continue;
                }
                else if (records[i] == "\n")
                {
                    if (isHeader)
                    {
                        isHeader = false;
                    }
                    else
                    {
                        date = null;
                        allExchangeRates.AddRange(exchangeRates);
                    }
                    continue;
                }

                if (isHeader)
                {
                    int quantity = int.Parse(records[i]);
                    var code = records[++i];

                    currencies.Add(new CzechNationalBankCurrency()
                    {
                        Code = code,
                        Quantity = quantity
                    });
                }
                else if (date == null)
                {
                    date = DateTime.Parse(records[i]);
                    exchangeRates = new List<CzechNationalBankExchangeRate>();
                    columnIndex = 0;
                }
                else
                {
                    var rate = decimal.Parse(records[i], CultureInfo.InvariantCulture);

                    exchangeRates.Add(new CzechNationalBankExchangeRate()
                    {
                        Date = date.Value,
                        Rate = rate,
                        CzechNationalBankCurrency = currencies[columnIndex]
                    });
                    columnIndex++;
                }
            }

            return allExchangeRates;
        }
    }
}
