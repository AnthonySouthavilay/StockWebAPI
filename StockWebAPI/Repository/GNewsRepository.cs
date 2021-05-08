using Newtonsoft.Json;
using StockWebAPI.Helpers;
using StockWebAPI.Models.GNews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StockWebAPI.Repository
{
    public class GNewsRepository
    {
        private const string token = "b9f2a63c4a6767e1f59d49d20b16319e";
        private readonly string defaultEndDate = DateTime.Now.ToString("yyyy-MM-dd");
        private readonly string defaultStartDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
        private readonly HttpClient _httpClient;

        public GNewsRepository(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<IEnumerable<GNewsCompanyNews.Article>> GetGNewsAsync(string symbol)
        {
            string apiEndpoint = "search";
            Uri requestUri = RequestUriHelper(apiEndpoint, symbol, defaultStartDate, defaultEndDate);
            try
            {
                GNewsCompanyNews.GNewsModel newsModel = await _httpClient.GetFromJsonAsync<GNewsCompanyNews.GNewsModel>(requestUri);
                return newsModel.articles;
            }
            catch (Exception ex)
            {
                throw new ApiException($"There was an issue retrieving company information due to: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<GNewsCompanyNews.Article>> GetGNewsAsync(string symbol, string startDate, string endDate)
        {
            string apiEndpoint = "search";
            Uri requestUri = RequestUriHelper(apiEndpoint, symbol, startDate, endDate);
            try
            {
                GNewsCompanyNews.GNewsModel newsModel = await _httpClient.GetFromJsonAsync<GNewsCompanyNews.GNewsModel>(requestUri);
                return newsModel.articles;
            }
            catch(Exception ex)
            {
                throw new ApiException($"There was an issue retrieving company information due to: {ex.Message}", ex);
            }
        }

        private static Uri RequestUriHelper(string apiEndpoint, string symbol, string startDate, string endDate)
        {
            string apiBaseKey = "GNews";
            string baseUrl = apiBaseKey.GetBaseUrl();
            return new Uri($"{baseUrl}{apiEndpoint}?q={symbol}&token={token}&lang=en&country=us&from={startDate}T00:00:00Z&to={endDate}T23:59:00Z");
        }
    }
}
