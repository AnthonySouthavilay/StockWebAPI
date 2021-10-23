using StockWebAPI.Helpers;
using StockWebAPI.Models.FinancialModelingPrep;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StockWebAPI.Repository
{
    public class FmpRepository
    {
        private readonly HttpClient _httpClient;

        public FmpRepository(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<FMPCompanyRating[]> GetCompanyRatingAsync(string symbol)
        {
            string endpoint = "rating";
            try
            {
                Uri requestUri = RequestUriHelper(endpoint, symbol);
                return await _httpClient.GetFromJsonAsync<FMPCompanyRating[]>(requestUri);
            }
            catch (Exception ex)
            {
                throw new ApiException($"There was an issue retrieving quote information due to: {ex.Message}", ex);
            }
        }

        private static Uri RequestUriHelper(string endpoint, string symbol)
        {
            string apiBaseKey = "FinancialModelingGroup";
            string baseUrl = apiBaseKey.GetBaseUrl();
            Uri uri = new($"{baseUrl}{endpoint}/{symbol}?apikey={apiKey}");
            return uri;
        }
    }
}