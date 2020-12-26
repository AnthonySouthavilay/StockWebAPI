using StockWebAPI.Models;
using StockWebAPI.Models.AlphaVantage;
using StockWebAPI.Models.IEXCloud;
using StockWebAPI.Repository;
using StockWebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StockWebAPI.Service
{
    public class CompanyService
    {
        private readonly HttpClient _httpClient;
        private readonly IEXRepository _iEXRepository;
        private readonly AlphaVantageRepository _alphaVantageRepository;

        public CompanyService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
            this._iEXRepository = new IEXRepository(_httpClient);
            this._alphaVantageRepository = new AlphaVantageRepository(_httpClient);
        }

        public async Task<CompanyProfileViewModel> GetCompanyProfileAsync(string symbol)
        {
            CompanyProfile companyProfile;
            CompanyProfileViewModel companyProfileViewModel = new CompanyProfileViewModel();
            if (IsValidSymbol(symbol))
            {
                companyProfile = await _iEXRepository.GetCompanyInfoAsync(symbol);
                return companyProfileViewModel.ConvertToCompanyProfileViewModel(companyProfile);
            }
            throw new ArgumentException($"{symbol} is not a valid stock symbol");
        }
        public async Task<CompanySummaryViewModel> GetCompanySummaryAsync(string symbol)
        {
            CompanySummaryViewModel companySummaryViewModel = new CompanySummaryViewModel();

            if (IsValidSymbol(symbol))
            {
                try
                {
                    IEXQuote iEXQuote = await _iEXRepository.GetQuoteAsync("23");
                    return companySummaryViewModel.ConvertToCompanySummaryViewModel(iEXQuote);
                }
                catch
                {
                    CompanyKeyStats keyStats = await _alphaVantageRepository.GetKeyInformationAsync(symbol);
                    AlphaVantageQuote alphaVantageQuote = await _alphaVantageRepository.GetQuote(symbol);
                    return companySummaryViewModel.ConvertToCompanySummaryViewModel(keyStats, alphaVantageQuote);
                }
            }
            throw new ArgumentException($"{symbol} is not a valid stock symbol");
        }
        private static bool IsValidSymbol(string input)
        {
            Regex rgx = new Regex("[^A-Za-z0-9]");
            if (!rgx.IsMatch(input) && !string.IsNullOrEmpty(input))
            {
                return true;
            }
            return false;
        }

    }
}
