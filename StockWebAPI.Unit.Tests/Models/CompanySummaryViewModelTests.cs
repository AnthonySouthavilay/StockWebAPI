using FluentAssertions.Execution;
using NUnit.Framework;
using StockWebAPI.ViewModels;
using FluentAssertions;
using StockWebAPI.Models.AlphaVantage;
using StockWebAPI.Models.IEXCloud;

namespace StockWebAPI.Unit.Tests.Models
{
    class CompanySummaryViewModelTests
    {
        private readonly CompanySummaryViewModel companySummaryViewModel = new();

        [Test]
        public void ConvertToCompanySummaryViewModel_Quote_ReturnsViewModel()
        {
            IexQuote quote = new()
            {
                Symbol = "TANK",
                CompanyName = "Tank Southy LLC",
                LatestPrice = 101.30,
                IexBidPrice = (int?)101.00
            };
            CompanySummaryViewModel result = companySummaryViewModel.ConvertToCompanySummaryViewModel(quote);
            using (new AssertionScope())
            {
                result.Should().NotBeNull();
                result.Price.Should().Be(quote.LatestPrice);
            }
        }

        [Test]
        public void ConvertToCompanySummaryViewModel_CompanyKeyStats_ReturnsViewModel()
        {
            AlphaVantageCompanyKeyStats keyStats = new() 
            {
                Symbol = "TANK",
                Name = "Tank Southy LLC",
                EPS = "101.3",
                _52WeekHigh = "106.11",
                _52WeekLow = "100.34",
                MarketCapitalization = "100",
                PERatio = "22"
            };
            AlphaVantageGlobalQuote alphaVantageQuote = new()
            {
                GlobalQuote = new AlphaVantageQuote()
                {
                    Open = "125.0000",
                    ChangePercent = "0.6376%",
                    Price = "124.6900",
                    High = "125.1000",
                    Low = "124.2100",
                    Volume = "100",
                    PreviousClose = "123.9000",
                    Change = "0.7900",
                }
            };
            CompanySummaryViewModel result = companySummaryViewModel.ConvertToCompanySummaryViewModel(keyStats, alphaVantageQuote.GlobalQuote);
            using (new AssertionScope())
            {
                result.Should().NotBeNull();
                result.Symbol.Should().Be("TANK");
                result.OpenPrice.Should().Be(125.00);
            }
        }
    }
}
