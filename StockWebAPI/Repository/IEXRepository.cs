using StockWebAPI.Models.IEXCloud;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StockWebAPI.Repository
{
    public class IEXRepository
    {
        private static string _baseUrl = "https://cloud.iexapis.com/stable/";
        private const string token = "pk_4a54de4d315647e0a424c2238d17891d";
        private HttpClient _httpClient;

        public IEXRepository(HttpClient httpClient)
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

        public async Task<IEXQuote> GetQuoteAsync(string symbol)
        {
            IEXQuote quote;
            string apiEndPoint = "quote";
            Uri requestUri = ApiUriHelper(apiEndPoint, symbol);
            try
            {
                quote = await _httpClient.GetFromJsonAsync<IEXQuote>(requestUri);
                return quote;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private static Uri ApiUriHelper(string apiEndpoint, string symbol)
        {
            Uri uri = new Uri($"{_baseUrl}stock/{symbol}/{apiEndpoint}?token={token}");
            return uri;
        }
    }
}