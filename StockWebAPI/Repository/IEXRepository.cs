using StockWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StockWebAPI.Repository
{
    public class IEXRepository
    {
        public CompanyInfo GetCompanyInfo(string stockSymbol)
        {
            if (ContainsSpecialCharacter(stockSymbol))
            {
                throw new ArgumentException($"{stockSymbol} is not a valid stock symbol");
            }
            return new CompanyInfo()
            {
                symbol = stockSymbol
            };
        }

        private static bool ContainsSpecialCharacter(string input)
        {
            Regex rgx = new Regex("[^A-Za-z0-9]");
            return rgx.IsMatch(input);
        }
    }
}
