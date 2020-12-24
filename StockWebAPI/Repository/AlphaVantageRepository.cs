using StockWebAPI.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StockWebAPI.Repository
{
    public class AlphaVantageRepository
    {
        private readonly HttpClient _httpClient;
        private const string _baseUrl = "https://www.alphavantage.co/query?function=";
        private const string apiKey = "&apikey=Y28C2P9CKJJP6OZ8";
        public AlphaVantageRepository(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<CompanyKeyStats> GetKeyInformationAsync(string symbol)
        {
            CompanyKeyStats companyKeyStats = new CompanyKeyStats();
            string apiEndpoint = "OVERVIEW&symbol=IBM";
            var requestUri = ApiUriHelper(apiEndpoint);

            try
            {
                companyKeyStats = await _httpClient.GetFromJsonAsync<CompanyKeyStats>(requestUri);
                return companyKeyStats;
            }
            catch (Exception)
            {
                throw new Exception("Unknown symbol");
            }
        }

        private static Uri ApiUriHelper(string apiEndpoint)
        {
            Uri uri = new Uri($"{_baseUrl}{apiEndpoint}{apiKey}");
            return uri;            
        }






    }
}