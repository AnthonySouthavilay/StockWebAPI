using FluentAssertions;
using NUnit.Framework;
using StockWebAPI.Models.Finnhub;
using StockWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StockWebAPI.Unit.Tests.Repositories
{
    class FinnhubRepositoryTests
    {
        private FinnhubRepository finnhubRepo;
        [Test]
        public async Task GetCompanyNews_ValidSymbol_ReturnsCompanyNewsModelAsync()
        {
            string symbol = "AAPL";
            HttpClient realHttp = new HttpClient();
            finnhubRepo = new FinnhubRepository(realHttp);
            CompanyNews[] result = await finnhubRepo.GetCompanyNewsAsync(symbol);
            result.Should().NotBeNull();
        }
    }
}
