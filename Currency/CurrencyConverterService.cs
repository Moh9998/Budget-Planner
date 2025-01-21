using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Currency
{
    public class CurrencyConverterService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "0d9f8a4c83-56cd52ecf1-sqfqma"; // Replace with your FastForex API key

        public CurrencyConverterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> GetConversionRateAsync(string fromCurrency, string toCurrency)
        {
            var response = await _httpClient.GetFromJsonAsync<ConversionResponse>($"https://api.fastforex.io/fetch-one?from={fromCurrency}&to={toCurrency}&api_key={_apiKey}");
            return response?.Result[toCurrency] ?? 1.0m;
        }

        public async Task<List<string>> GetCurrenciesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CurrenciesResponse>($"https://api.fastforex.io/currencies?api_key={_apiKey}");
            return response?.Currencies.Keys.ToList( ) ?? new List<string>( );
        }

        private class ConversionResponse
        {
            public Dictionary<string, decimal> Result
            {
                get; set;
            }
        }

        private class CurrenciesResponse
        {
            public Dictionary<string, string> Currencies
            {
                get; set;
            }
        }
    }
}

