using StockWebAPI.Models;
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
        private  HttpClient _httpClient;
        private IEXRepository iEXRepository;

        public CompanyService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
            this.iEXRepository = new IEXRepository(_httpClient);
        }

        public async Task<CompanyProfileViewModel> GetCompanyProfileAsync(string symbol)
        {
            CompanyProfile companyProfile;
            CompanyProfileViewModel companyProfileViewModel = new CompanyProfileViewModel();
            if (IsValidSymbol(symbol))
            {
                companyProfile = await iEXRepository.GetCompanyInfoAsync(symbol);
                companyProfileViewModel = companyProfileViewModel.ConvertToCompanyProfileViewModel(companyProfile);
                return companyProfileViewModel;
            }
            throw new ArgumentException($"{symbol} is not a valid stock symbol");
        }

        // IEX - Key Stats, 5 per call per symbol for full stats
        // AlphaAdvantage - Company Overview

        // IEX - Logo


        // Check Finnhub, https://developer.tradier.com/ ,
        // Historical Data - https://marketstack.com/


        public async Task<CompanySummaryViewModel> GetCompanySummaryAsync(string symbol)
        {
            CompanySummaryViewModel companySummaryViewModel = new CompanySummaryViewModel();

            if (IsValidSymbol(symbol))
            {
                IEXQuote iEXQuote = await iEXRepository.GetQuoteAsync(symbol);
                companySummaryViewModel = companySummaryViewModel.ConvertToCompanySummaryViewModel(iEXQuote);
                return companySummaryViewModel;
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
