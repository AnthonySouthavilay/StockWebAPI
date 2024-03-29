﻿using StockWebAPI.Helpers;
using StockWebAPI.Models.IEXCloud;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StockWebAPI.Repository
{
    public class IexRepository
    {
        private const string token = "";
        private readonly HttpClient _httpClient;

        public IexRepository(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<IEXCompanyProfile> GetCompanyInfoAsync(string stockSymbol)
        {
            IEXCompanyProfile profile;
            string apiEndPoint = "company";
            Uri requestUri = RequestUriHelper(apiEndPoint, stockSymbol);
            try
            {
                profile = await _httpClient.GetFromJsonAsync<IEXCompanyProfile>(requestUri);
                return profile;
            }
            catch(Exception ex)
            {
                throw new ApiException($"There was an issue retrieving company information due to: {ex.Message}", ex);
            }
        }

        public async Task<IexQuote> GetQuoteAsync(string symbol)
        {
            IexQuote quote;
            string apiEndPoint = "quote";
            try
            {
                Uri requestUri = RequestUriHelper(apiEndPoint, symbol);
                quote = await _httpClient.GetFromJsonAsync<IexQuote>(requestUri);
                return quote;
            }
            catch (Exception ex)
            {
                throw new ApiException($"There was an issue retrieving quote information due to: {ex.Message}", ex);
            }

        }

        private static Uri RequestUriHelper(string apiEndpoint, string symbol)
        {
            string apiBaseKey = "Iex";
            string baseUrl = apiBaseKey.GetBaseUrl();
            Uri uri = new($"{baseUrl}stock/{symbol}/{apiEndpoint}?token={token}");
            return uri;
        }
    }
}