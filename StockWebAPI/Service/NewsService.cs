using StockWebAPI.ViewModels;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using StockWebAPI.Models.Finnhub;
using StockWebAPI.Repository;
using System.Collections.Generic;
using StockWebAPI.Helpers;

namespace StockWebAPI.Service
{
    public class NewsService
    {
        private readonly HttpClient _httpClient;
        private readonly FinnhubRepository _finnhubRepo;
        public NewsService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
            this._finnhubRepo = new FinnhubRepository(_httpClient);
        }

        public async Task<CompanyNewsViewModel[]> GetCompanyNewsByDateRangeAsync(string symbol, string requestedStartDate, string requestedEndDate)
        {
            DateTime startDate = DateTime.Parse(CheckForEncodedForwardSlash(requestedStartDate));
            DateTime endDate = DateTime.Parse(CheckForEncodedForwardSlash(requestedEndDate));

            FinnhubCompanyNews[] companyNews;
            List<CompanyNewsViewModel> companyNewsViewModel = new();
            if (symbol.IsValid())
            {
                try
                {
                    companyNews = await _finnhubRepo.GetCompanyNewsAsync(symbol, startDate, endDate);
                    foreach (FinnhubCompanyNews news in companyNews)
                    {
                        companyNewsViewModel.Add(new CompanyNewsViewModel().ConvertToCompanyNewsViewModel(news));
                    }
                    return companyNewsViewModel.ToArray();
                }
                catch (ApiException ex)
                {
                    throw new ApiException($"There was an issue retrieving company news.", ex);
                }
            }
            throw new ArgumentException($"{symbol} is not a valid stock symbol");
        }

        public async Task<CompanyNewsViewModel[]> GetCurrentCompanyNewsAsync(string symbol)
        {
            FinnhubCompanyNews[] companyNews;
            List<CompanyNewsViewModel> companyNewsViewModel = new();
            if (symbol.IsValid())
            {
                try
                {
                    companyNews = await _finnhubRepo.GetCurrentCompanyNews(symbol);
                    foreach (FinnhubCompanyNews news in companyNews)
                    {
                        companyNewsViewModel.Add(new CompanyNewsViewModel().ConvertToCompanyNewsViewModel(news));
                    }
                    return companyNewsViewModel.ToArray();
                }
                catch (ApiException ex)
                {
                    throw new ApiException($"There was an issue retrieving current news.", ex);
                }
            }
            throw new ArgumentException($"{symbol} is not a valid stock symbol");
        }
        private static string CheckForEncodedForwardSlash(string inputString)
        {
            return inputString.Contains("%2F") ? inputString.Replace("%2F", "/") : inputString;
        }
    }
}
