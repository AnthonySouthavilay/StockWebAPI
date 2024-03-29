﻿using StockWebAPI.Helpers;
using StockWebAPI.Models.AlphaVantage;
using StockWebAPI.Models.IEXCloud;
using StockWebAPI.Repository;
using StockWebAPI.ViewModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockWebAPI.Service
{
    public class CompanyService
    {
        private readonly HttpClient _httpClient;
        private readonly IexRepository _iEXRepository;
        private readonly AlphaVantageRepository _alphaVantageRepository;

        public CompanyService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
            this._iEXRepository = new IexRepository(_httpClient);
            this._alphaVantageRepository = new AlphaVantageRepository(_httpClient);
        }

        public async Task<CompanyProfileViewModel> GetCompanyProfileAsync(string symbol)
        {
            IEXCompanyProfile companyProfile;
            CompanyProfileViewModel companyProfileViewModel = new();
            if (symbol.IsValid())
            {
                try
                {
                    companyProfile = await _iEXRepository.GetCompanyInfoAsync(symbol);
                    return companyProfileViewModel.ConvertToCompanyProfileViewModel(companyProfile);
                }
                catch (ApiException)
                {
                    return await CompanyProfileApiBackupAsync(symbol);
                }

            }
            throw new ArgumentException($"{symbol} is not a valid stock symbol");
        }
        public async Task<CompanySummaryViewModel> GetCompanySummaryAsync(string symbol)
        {
            CompanySummaryViewModel companySummaryViewModel = new();
            if (symbol.IsValid())
            {
                try
                {
                    IexQuote iEXQuote = await _iEXRepository.GetQuoteAsync(symbol);
                    return companySummaryViewModel.ConvertToCompanySummaryViewModel(iEXQuote);
                }
                catch (ApiException)
                {
                    return await CompanySummaryApiBackupAsync(symbol);
                }
            }
            throw new ArgumentException($"{symbol} is not a valid stock symbol");
        }

        private async Task<CompanyProfileViewModel> CompanyProfileApiBackupAsync(string symbol)
        {
            CompanyProfileViewModel companyProfileViewModel = new();
            try
            {
                AlphaVantageCompanyKeyStats keyStats = await _alphaVantageRepository.GetKeyInformationAsync(symbol);
                return companyProfileViewModel.ConvertToCompanyProfileViewModel(keyStats);
            }
            catch (ApiException ex)
            {
                throw new ApiException($"There was an issue retrieving company profile.", ex);
            }
        }

        private async Task<CompanySummaryViewModel> CompanySummaryApiBackupAsync(string symbol)
        {
            CompanySummaryViewModel companySummaryViewModel = new();
            try
            {
                AlphaVantageCompanyKeyStats keyStats = await _alphaVantageRepository.GetKeyInformationAsync(symbol);
                AlphaVantageQuote alphaVantageQuote = await _alphaVantageRepository.GetQuote(symbol);
                return companySummaryViewModel.ConvertToCompanySummaryViewModel(keyStats, alphaVantageQuote);
            }
            catch (ApiException ex)
            {
                throw new ApiException($"There was an issue retrieving company summary.", ex);
            }
        }
    }
}
