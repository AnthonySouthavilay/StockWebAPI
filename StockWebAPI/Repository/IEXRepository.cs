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
        public CompanyProfile GetCompanyInfo(string stockSymbol)
        {
            return new CompanyProfile()
            {
                symbol = stockSymbol
            };
        }


    }
}
