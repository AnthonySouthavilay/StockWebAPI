using StockWebAPI.Helpers;
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
            CompanyProfile companyProfile;
            CompanyProfileViewModel companyProfileViewModel = new CompanyProfileViewModel();
            if (symbol.IsValid())
            {
                try
                {
                    companyProfile = await _iEXRepository.GetCompanyInfoAsync(symbol);
                    return companyProfileViewModel.ConvertToCompanyProfileViewModel(companyProfile);
                }
                catch (ApiException)
                {
                    CompanyKeyStats keyStats = await _alphaVantageRepository.GetKeyInformationAsync(symbol);
                    return companyProfileViewModel.ConvertToCompanyProfileViewModel(keyStats);
                }

            }
            throw new ArgumentException($"{symbol} is not a valid stock symbol");
        }
        public async Task<CompanySummaryViewModel> GetCompanySummaryAsync(string symbol)
        {
            CompanySummaryViewModel companySummaryViewModel = new CompanySummaryViewModel();
            if (symbol.IsValid())
            {
                try
                {
                    IexQuote iEXQuote = await _iEXRepository.GetQuoteAsync(symbol);
                    return companySummaryViewModel.ConvertToCompanySummaryViewModel(iEXQuote);
                }
                catch (ApiException)
                {
                    CompanyKeyStats keyStats = await _alphaVantageRepository.GetKeyInformationAsync(symbol);
                    AlphaVantageQuote alphaVantageQuote = await _alphaVantageRepository.GetQuote(symbol);
                    return companySummaryViewModel.ConvertToCompanySummaryViewModel(keyStats, alphaVantageQuote);
                }
            }
            throw new ArgumentException($"{symbol} is not a valid stock symbol");
        }
    }
}
