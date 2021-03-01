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
        private HttpClient _httpClient;
        private const string token = "b9f2a63c4a6767e1f59d49d20b16319e";

        public GNewsRepository(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<IEnumerable<GNewsCompanyNews.Article>> GetGNewsAsync(string symbol)
        {
            GNewsCompanyNews.GNewsModel newsModel;
            string apiEndpoint = "search";
            Uri requestUri = RequestUriHelper(apiEndpoint, symbol);
            newsModel = await _httpClient.GetFromJsonAsync<GNewsCompanyNews.GNewsModel>(requestUri);
            return newsModel.articles;
        }

        private static Uri RequestUriHelper(string apiEndpoint, string symbol)
        {
            string apiBaseKey = "GNews";
            string baseUrl = apiBaseKey.GetBaseUrl();
            Uri uri = new Uri($"{baseUrl}{apiEndpoint}?q={symbol}&token={token}&lang=en&country=us");
            return uri;
        }
    }
}
