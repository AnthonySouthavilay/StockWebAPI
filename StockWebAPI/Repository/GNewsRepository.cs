using Newtonsoft.Json;
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

        public GNewsRepository(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<IEnumerable<GNewsCompanyNews.Article>> GetGNewsAsync(string validSymbol)
        {
            GNewsCompanyNews.GNewsModel newsModel;
            string apiEndpoint = "search";
            newsModel = await _httpClient.GetFromJsonAsync<GNewsCompanyNews.GNewsModel>(new Uri("https://gnews.io/api/v4/search?q=AAPL&token=b9f2a63c4a6767e1f59d49d20b16319e&lang=en&country=us"));
            return newsModel.articles;
        }
    }
}
