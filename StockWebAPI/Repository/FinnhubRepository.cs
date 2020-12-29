using StockWebAPI.Models.Finnhub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StockWebAPI.Repository
{
    public class FinnhubRepository
    {
        private HttpClient _httpClient;
        private string _requestUrl = "https://finnhub.io/api/v1/company-news?symbol=AAPL&from=2020-04-30&to=2020-05-01&token=";
        private const string _token = "bvl85tn48v6sqkppa030";

        public FinnhubRepository(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<CompanyNews> GetCompanyNewsAsync(string symbol)
        {
            CompanyNews companyNews;
            try
            {
                companyNews = await _httpClient.GetFromJsonAsync<CompanyNews>(new Uri($"{_requestUrl}{_token}"));
                return companyNews;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
