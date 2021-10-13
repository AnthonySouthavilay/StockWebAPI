using StockWebAPI.Helpers;
using StockWebAPI.Models.SocialMedia;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StockWebAPI.Repository
{
    public class SocialMediaRepository
    {
        private readonly HttpClient _httpClient;

        public SocialMediaRepository(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<IEnumerable<WsbInfo>> GetWallStreetBetsRecommendationsAsync()
        {
            string apiBaseKey = "WallStreetBets";
            string requestUri = apiBaseKey.GetBaseUrl();

            try
            {
                return await _httpClient.GetFromJsonAsync<WsbInfo[]>(requestUri);
            }
            catch (Exception ex)
            {
                throw new ApiException($"There was an issue retrieving WallStreetBets information due to: {ex.Message}", ex);
            }
        }
    }
}