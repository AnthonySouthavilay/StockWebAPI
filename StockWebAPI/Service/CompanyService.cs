using StockWebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StockWebAPI.Service
{
    public class CompanyService
    {
        public CompanyProfileViewModel GetCompanyProfile(string symbol)
        {
            if (IsValidSymbol(symbol))
            {
                return new CompanyProfileViewModel() { Name = "Tank Southy LLC" };
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
