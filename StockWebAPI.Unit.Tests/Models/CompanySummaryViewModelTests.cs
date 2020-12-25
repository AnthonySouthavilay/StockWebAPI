using FluentAssertions.Execution;
using NUnit.Framework;
using StockWebAPI.Models;
using StockWebAPI.ViewModels;
using FluentAssertions;
using StockWebAPI.Models.IEXCloud;
using StockWebAPI.Models.AlphaVantage;

namespace StockWebAPI.Unit.Tests.Models
{
    class CompanySummaryViewModelTests
    {
        private readonly CompanySummaryViewModel companySummaryViewModel = new CompanySummaryViewModel();

        [Test]
        public void ConvertToCompanySummaryViewModel_Quote_ReturnsViewModel()
        {
            Quote quote = new Quote()
            {
                symbol = "TANK",
                companyName = "Tank Southy LLC",
                latestPrice = 101.30,
                iexBidPrice = (int)101
            };
            CompanySummaryViewModel result = companySummaryViewModel.ConvertToCompanySummaryViewModel(quote);
            using (new AssertionScope())
            {
                result.Should().NotBeNull();
                result.Price.Should().Be(quote.latestPrice);
            }
        }

        [Test]
        public void ConvertToCompanySummaryViewModel_CompanyKeyStats_ReturnsViewModel()
        {
            CompanyKeyStats keyStats = new CompanyKeyStats() 
            {
                Symbol = "TANK",
                Name = "Tank Southy LLC",
                EPS = "101.3",
                _52WeekHigh = "106.11",
                _52WeekLow = "100.34",
                MarketCapitalization = "100",
                PERatio = "22"
            };
            CompanySummaryViewModel result = companySummaryViewModel.ConvertToCompanySummaryViewModel(keyStats);
            using (new AssertionScope())
            {
                result.Should().NotBeNull();
                result.Symbol.Should().Be("TANK");
            }

        }
    }
}
