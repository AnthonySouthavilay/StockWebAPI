using StockWebAPI.Models;
using StockWebAPI.Models.IEXCloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StockWebAPI.Repository
{
    public class IEXRepository
    {
        private Uri baseUri = new Uri("https://cloud.iexapis.com/stable/");
        private const string token = "pk_4a54de4d315647e0a424c2238d17891d";
        private HttpClient _httpClient;

        public IEXRepository(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<CompanyProfile> GetCompanyInfoAsync(string stockSymbol)
        {
            CompanyProfile profile = new CompanyProfile();
            string apiEndPoint = $"stock/{stockSymbol}/company?token={token}";
            var requestUri = new Uri(baseUri, apiEndPoint);
            
            try
            {
                profile = await _httpClient.GetFromJsonAsync<CompanyProfile>(requestUri);
                // fix employee property; doesnt seem to map
                return profile;
            }
            catch(Exception)
            {
                throw new Exception("Unknown symbol");
            }
        }


    }
}