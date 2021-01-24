using Newtonsoft.Json;
using StockWebAPI.Models.AlphaVantage;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using StockWebAPI.Helpers;

namespace StockWebAPI.Repository
{
    public class AlphaVantageRepository
    {
        private readonly HttpClient _httpClient;
        private const string apiKey = "Y28C2P9CKJJP6OZ8";
        public AlphaVantageRepository(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<CompanyKeyStats> GetKeyInformationAsync(string symbol)
        {
            string apiEndpoint = "OVERVIEW";
            Uri requestUri = ApiUriHelper(apiEndpoint, symbol);
            try
            {
                return await _httpClient.GetFromJsonAsync<CompanyKeyStats>(requestUri);
            }
            catch (Exception)
            {
                throw new Exception("Unknown symbol");
            }
        }
        public async Task<AlphaVantageQuote> GetQuote(string symbol)
        {
            string apiEndpoint = "GLOBAL_QUOTE";
            try
            {
                Uri requestUri = ApiUriHelper(apiEndpoint, symbol);
                string jsonString = await _httpClient.GetStringAsync(requestUri);
                AlphaVantageGlobalQuote globalQuote = JsonConvert.DeserializeObject<AlphaVantageGlobalQuote>(jsonString);
                return globalQuote.GlobalQuote;
            }
            catch (Exception)
            {
                throw new Exception("Unknown symbol");
            }

        }
        private static Uri ApiUriHelper(string apiEndpoint, string symbol)
        {
            string apiBaseKey = "AlphaAdvantage";
            string _baseUrl = apiBaseKey.GetBaseUrl();
            Uri uri = new Uri($"{_baseUrl}{apiEndpoint}&symbol={symbol}&apikey={apiKey}");
            return uri;
        }

    }
}