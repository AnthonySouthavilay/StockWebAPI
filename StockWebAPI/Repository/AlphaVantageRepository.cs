using StockWebAPI.Models;
using System;
using System.Net.Http;

namespace StockWebAPI.Repository
{
    public class AlphaVantageRepository
    {
        private readonly HttpClient _httpClient;
        public AlphaVantageRepository(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public CompanyKeyStats GetKeyInformation(string symbol)
        {
            CompanyKeyStats companyKeyStats = new CompanyKeyStats();
            string api = "https://www.alphavantage.co/query?function=OVERVIEW&symbol=IBM&apikey=Y28C2P9CKJJP6OZ8";
            var requestUri = new Uri(api);

            try
            {
                companyKeyStats = await _httpClient
            }
        }







    }
}