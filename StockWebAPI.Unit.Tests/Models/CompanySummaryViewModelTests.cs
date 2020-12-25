using FluentAssertions.Execution;
using NUnit.Framework;
using StockWebAPI.Models;
using StockWebAPI.ViewModels;
using FluentAssertions;
using StockWebAPI.Models.IEXCloud;

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
    }
}
