using NUnit.Framework;
using StockWebAPI.Models;
using StockWebAPI.Repository;
using FluentAssertions;
using System;

namespace StockWebAPI.Unit.Tests
{
    public class IEXRepositoryTests
    {
        private IEXRepository iexRepo = new IEXRepository();
        private readonly string testStockSymbol = "TANK";

        [Test]
        public void GetCompanyInformation_ValidSymbol_ReturnsModel()
        {
            var result = iexRepo.GetCompanyInfo(testStockSymbol);
            result.Should().NotBeNull();
        }
    }
}