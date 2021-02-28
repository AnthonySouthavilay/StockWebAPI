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

        public async Task<CompanyNewsViewModel[]> GetCompanyNewsAsync(string symbol, string requestedStartDate, string requestedEndDate)
        {
            DateTime startDate = DateTime.Parse(requestedStartDate);
            DateTime endDate = DateTime.Parse(requestedEndDate);

            FinnhubCompanyNews[] companyNews;
            List<CompanyNewsViewModel> companyNewsViewModel = new List<CompanyNewsViewModel>();
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
                catch
                {
                    throw new Exception($"There was an issue retrieving company news.");
                }
            }
            throw new ArgumentException($"{symbol} is not a valid stock symbol");
        }

        public async Task<CompanyNewsViewModel[]> GetCompanyNewsAsync(string symbol)
        {
            FinnhubCompanyNews[] companyNews;
            List<CompanyNewsViewModel> companyNewsViewModel = new List<CompanyNewsViewModel>();
            if (symbol.IsValid())
            {
                try
                {
                    companyNews = await _finnhubRepo.GetCompanyNewsAsync(symbol);
                    foreach (FinnhubCompanyNews news in companyNews)
                    {
                        companyNewsViewModel.Add(new CompanyNewsViewModel().ConvertToCompanyNewsViewModel(news));
                    }
                    return companyNewsViewModel.ToArray();
                }
                catch
                {
                    throw new Exception($"There was an issue retrieving company news.");
                }
            }
            throw new ArgumentException($"{symbol} is not a valid stock symbol");
        }

        //Finnhub - Market News - General

        //Finnhub - Market News - Crypto

        //
    }
}
