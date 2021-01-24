using StockWebAPI.Helpers;
using StockWebAPI.Models.IEXCloud;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StockWebAPI.Repository
{
    public class IexRepository
    {
        private const string token = "pk_4a54de4d315647e0a424c2238d17891d";
        private readonly HttpClient _httpClient;

        public IexRepository(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<CompanyProfile> GetCompanyInfoAsync(string stockSymbol)
        {
            CompanyProfile profile;
            string apiEndPoint = "company";
            Uri requestUri = ApiUriHelper(apiEndPoint, stockSymbol);
            try
            {
                profile = await _httpClient.GetFromJsonAsync<CompanyProfile>(requestUri);
                return profile;
            }
            catch(Exception)
            {
                throw new Exception("Unknown symbol");
            }
        }

        public async Task<IexQuote> GetQuoteAsync(string symbol)
        {
            IexQuote quote;
            string apiEndPoint = "quote";
            try
            {
                Uri requestUri = ApiUriHelper(apiEndPoint, symbol);
                quote = await _httpClient.GetFromJsonAsync<IexQuote>(requestUri);
                return quote;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private static Uri ApiUriHelper(string apiEndpoint, string symbol)
        {
            string apiBaseKey = "Iex";
            string baseUrl = apiBaseKey.GetBaseUrl();
            Uri uri = new Uri($"{baseUrl}stock/{symbol}/{apiEndpoint}?token={token}");
            return uri;
        }
    }
}