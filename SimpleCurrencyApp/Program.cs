using Microsoft.Extensions.Configuration;
using SimpleCurrencyApp.classes;
using System;

namespace SimpleCurrencyApp
{
    internal class Program
    {
        static async Task Main()
        {
            Console.WriteLine("Welcome to the currency converter app");

            // Get configuration data
            var config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
            string baseApi = config["BaseApi"] ?? string.Empty;
            string apiKey = config["ApiKey"] ?? string.Empty;

            // Load currency data
            Console.WriteLine("Loading data...");
            var loader = new CurrencyLoader(baseApi, apiKey);
            var converter = await loader.LoadCurrencyDataAsync();
                        
            // Run program
            bool carryOn = true;
            while (carryOn)
            {
                Console.WriteLine("Please enter: ");
                Console.WriteLine("1. For a list of available currencies");
                Console.WriteLine("2. To perform a currency conversion");
                Console.WriteLine("Any other key to quit");
                string input = Console.ReadLine() ?? string.Empty;
                if (input == "1")
                {
                    var details = converter.GetCurrencyData();
                    details.ForEach(d => Console.WriteLine(d));
                }
                else if (input == "2")
                {
                    // Get currency codes
                    Console.WriteLine("Please enter the currency code to convert from: ");
                    string convertFrom = GetCurrencyCodeInput(converter);
                    Console.WriteLine("Please enter the currency code to convert to: ");
                    string convertTo = GetCurrencyCodeInput(converter);
                    Console.WriteLine("Please enter the amount to convert: ");

                    // Get amount
                    string amountInput = Console.ReadLine()?.Trim() ?? string.Empty;
                    decimal amount = 0;
                    while (!decimal.TryParse(amountInput, out amount))
                    {
                        Console.WriteLine($"{amountInput} is not a amount. Please try again.");
                        amountInput = Console.ReadLine()?.Trim() ?? string.Empty;
                    }

                    decimal convertedAmount = converter.Convert(convertFrom, convertTo, amount);
                    Console.WriteLine($"{amount:N2} {convertFrom} is {convertedAmount:N2} {convertTo}");
                }
                else
                {
                    carryOn = false;
                }
                Console.WriteLine();
            }

        }

        private static string GetCurrencyCodeInput(CurrencyConverter converter)
        {
            string code = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;
            while (!converter.CurrencyAvailable(code))
            {
                Console.WriteLine($"{code} is not a valid code. Please try again.");
                code = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;
            }
            return code;
        }


    }
}

