using NUnit.Framework;
using StockWebAPI.Models;
using StockWebAPI.Repository;
using FluentAssertions;
using System;
using System.Threading.Tasks;

namespace StockWebAPI.Unit.Tests
{
    public class IEXRepositoryTests
    {

        private IEXRepository iexRepo = new IEXRepository(new System.Net.Http.HttpClient());
        private readonly string testStockSymbol = "AAPL";
        string mockReturnedJson = "{\"Name\":\"John Doe\",\"Occupation\":\"gardener\",\"DateOfBirth\":{\"year\":1995,\"month\":11,\"day\":30}}";

        [Test]
        public async Task GetCompanyInformation_ValidSymbol_ReturnsModelAsync()
        {
            var result = await iexRepo.GetCompanyInfoAsync(testStockSymbol);
            result.Should().NotBeNull();
        }
    }
}