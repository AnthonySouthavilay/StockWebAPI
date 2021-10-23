using StockWebAPI.Helpers;
using StockWebAPI.Models.Finnhub;
using StockWebAPI.Unit.Tests.Repositories;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StockWebAPI.Repository
{
    public class FinnhubRepository
    {
        private readonly HttpClient _httpClient;
        private const string _token = "";
        private readonly static string apiBaseKey = "FinnHub";
        private readonly static string _baseUrl = apiBaseKey.GetBaseUrl();

        public FinnhubRepository(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<FinnhubCompanyNews[]> GetCompanyNewsAsync(string symbol, DateTime startDate, DateTime endDate)
        {
            FinnhubCompanyNews[] companyNews;
            string apiEndpoint = "company-news";
            Uri _requestUri;
            try
            {
                _requestUri = DateUriHelper(symbol, apiEndpoint, startDate, endDate);
                companyNews = await _httpClient.GetFromJsonAsync<FinnhubCompanyNews[]>(_requestUri);
                return companyNews;
            }
            catch(Exception ex)
            {
                throw new ApiException($"There was an issue retrieving company news with those dates due to: {ex.Message}", ex);
            }
        }

        public async Task<FinnhubCompanyNews[]> GetCurrentCompanyNews(string symbol)
        {
            FinnhubCompanyNews[] companyNews;
            string apiEndpoint = "company-news";
            try
            {
                Uri _requestUri = DateUriHelper(symbol, apiEndpoint);
                companyNews = await _httpClient.GetFromJsonAsync<FinnhubCompanyNews[]>(_requestUri);
                return companyNews;
            }
            catch (Exception ex)
            {
                throw new ApiException($"There was an issue retrieving current news due to: {ex.Message}", ex);
            }
        }

        public async Task<FinnhubRecommendationTrends[]> GetRecommendationTrendsAsync(string symbol)
        {
            FinnhubRecommendationTrends[] recommendation;
            string apiEndpoint = "recommendation";
            try
            {
                Uri _requestUri = new($"{_baseUrl}stock/{apiEndpoint}?symbol={symbol}&token={_token}");
                recommendation = await _httpClient.GetFromJsonAsync<FinnhubRecommendationTrends[]>(_requestUri);
                return recommendation;
            }
            catch(Exception ex)
            {
                throw new ApiException($"There was an issue retrieving recommendation trends due to: {ex.Message}", ex);
            }
        }

        private static Uri DateUriHelper(string symbol, string apiEndpoint, DateTime startDate = new(), DateTime endDate = new())
        {
            DateTime today = DateTime.Today;
            return startDate == DateTime.MinValue || endDate == DateTime.MinValue
                ? new Uri($"{_baseUrl}{apiEndpoint}?symbol={symbol}&from={today:yyyy-MM-dd}&to={today:yyyy-MM-dd}&token={_token}")
                : new Uri($"{_baseUrl}{apiEndpoint}?symbol={symbol}&from={startDate:yyyy-MM-dd}&to={endDate:yyyy-MM-dd}&token={_token}");
        }
    }
}
