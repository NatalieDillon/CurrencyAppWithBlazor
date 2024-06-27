using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCurrencyApp.classes
{
    public class CurrencyConverter
    {
        private Dictionary<string, Currency> _currencies;

        public CurrencyConverter(Dictionary<string, Currency> currencies)
        {
            _currencies = currencies;
        }

        public decimal Convert(string from, string to, decimal amount)
        {
            decimal convertedAmount = amount * (_currencies[to].Rate / _currencies[from].Rate);
            return convertedAmount;
        }

        public List<string> GetCurrencyData()
        {
            return _currencies.Values.Select(x => x.Details).ToList();
        }

        public List<Currency> GetCurrencyList()
        {
            return _currencies.Values.ToList();
        }

        public bool CurrencyAvailable(string code)
        {
            return _currencies.ContainsKey(code);
        }

    }
}
