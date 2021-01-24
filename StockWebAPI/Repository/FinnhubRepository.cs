using StockWebAPI.Helpers;
using StockWebAPI.Models.Finnhub;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StockWebAPI.Repository
{
    public class FinnhubRepository
    {
        private readonly HttpClient _httpClient;
        private const string _token = "bvl85tn48v6sqkppa030";

        public FinnhubRepository(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<CompanyNews[]> GetCompanyNewsAsync(string symbol, DateTime startDate, DateTime endDate)
        {
            CompanyNews[] companyNews;
            string apiEndpoint = "company-news";
            Uri _requestUri;
            try
            {
                _requestUri = RequestUriHelper(symbol, apiEndpoint, startDate, endDate);
                companyNews = await _httpClient.GetFromJsonAsync<CompanyNews[]>(_requestUri);
                return companyNews;
            }
            catch(Exception ex)
            {
                throw new ApiException($"There was an issue retrieving company news with those dates due to: {ex.Message}", ex);
            }
        }

        public async Task<CompanyNews[]> GetCompanyNewsAsync(string symbol)
        {
            CompanyNews[] companyNews;
            string apiEndpoint = "company-news";
            try
            {
                Uri _requestUri = RequestUriHelper(symbol, apiEndpoint);
                companyNews = await _httpClient.GetFromJsonAsync<CompanyNews[]>(_requestUri);
                return companyNews;
            }
            catch (Exception ex)
            {
                throw new ApiException($"There was an issue retrieving current news due to: {ex.Message}", ex);
            }
        }

        private static Uri RequestUriHelper(string symbol, string apiEndpoint, DateTime startDate = new (), DateTime endDate = new ())
        {
            string apiBaseKey = "FinnHub";
            string _baseUrl = apiBaseKey.GetBaseUrl();
            DateTime today = DateTime.Today;
            return startDate == DateTime.MinValue || endDate == DateTime.MinValue
                ? new Uri($"{_baseUrl}{apiEndpoint}?symbol={symbol}&from={today:yyyy-MM-dd}&to={today:yyyy-MM-dd}&token={_token}")
                : new Uri($"{_baseUrl}{apiEndpoint}?symbol={symbol}&from={startDate:yyyy-MM-dd}&to={endDate:yyyy-MM-dd}&token={_token}");
        }
    }
}
