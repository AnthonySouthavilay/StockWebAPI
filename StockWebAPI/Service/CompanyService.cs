﻿using StockWebAPI.Models;
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

        private static bool IsValidSymbol(string input)
        {
            Regex rgx = new Regex("[^A-Za-z0-9]");
            if(!rgx.IsMatch(input) && !string.IsNullOrEmpty(input))
            {
                return true;
            }
            return false;
        }
    }
}
