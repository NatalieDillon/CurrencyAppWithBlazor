using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SimpleCurrencyApp.classes
{
	public class CurrencyLoader
	{
		private readonly string _apiKey;
		private readonly HttpClient _httpClient;

		private readonly JsonSerializerOptions _options = new()
		{
			PropertyNameCaseInsensitive = true
		};

		public CurrencyLoader(string baseUrl, string apiKey)
		{
			_httpClient = new();
			_httpClient.BaseAddress = new Uri(baseUrl);
			_apiKey = apiKey;
		}

		private async Task<RatesDto> LoadRatesDataAsync()
		{
			// Creates the get request 
			string url = $"latest.json?app_id={_apiKey}";

			// Gets the response from the web service
			HttpResponseMessage response = await _httpClient.GetAsync(url);

			// Throws an exception if the request was not sucessful
			response.EnsureSuccessStatusCode();

			// Reads the response as a string (JSON format)
			string responseBody = await response.Content.ReadAsStringAsync();

			// Converts the JSON into an object, in this case RatesDto
			RatesDto result = JsonSerializer.Deserialize<RatesDto>(responseBody, _options) ?? new();

			return result;
		}

		private async Task<Dictionary<string, string>> LoadCurrencyInfoAsync()
		{
			string url = $"currencies.json?app_id={_apiKey}";
			HttpResponseMessage response = await _httpClient.GetAsync(url);
			response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();
			Dictionary<string, string> result = JsonSerializer.Deserialize<Dictionary<string, string>>(responseBody, _options) ?? new();
			return result;
		}

		public async Task<CurrencyConverter> LoadCurrencyDataAsync()
		{
			// Both tasks are started in parallel
			var ratesTask = LoadRatesDataAsync();
			var currencyTask = LoadCurrencyInfoAsync();

			// Both the tasks need to complete before processing can continue
			var rates = await ratesTask;
			var currencyData = await currencyTask;

			// Create the correct objects to return to the calling code
			Dictionary<string, Currency> currencies = new();
			foreach (var rate in rates.Rates)
			{
				string currencyName = currencyData[rate.Key];
				Currency currency = new(rate.Key, currencyName, rate.Value);
				currencies.Add(rate.Key, currency);
			}

			CurrencyConverter converter = new(currencies);

			return converter;
		}

	}
}