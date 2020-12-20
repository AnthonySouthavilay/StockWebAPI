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
            if (ContainsSpecialCharacter(symbol) || string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentException($"{symbol} is not a valid stock symbol");
            }
            return new CompanyProfileViewModel() { Name = "Tank Southy LLC" };
        }

        private static bool ContainsSpecialCharacter(string input)
        {
            Regex rgx = new Regex("[^A-Za-z0-9]");
            return rgx.IsMatch(input);
        }
    }
}
