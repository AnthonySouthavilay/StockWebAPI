using FluentAssertions;
using NUnit.Framework;
using StockWebAPI.Helpers;

namespace StockWebAPI.Unit.Tests.Helpers
{
    public class ExtensionsTests
    {
        [TestCase("AAPL")]
        [TestCase("T4NK")]
        public void IsValid_ValidSymbol_ReturnsTrue(string symbol)
        {
            bool result = symbol.IsValid();
            result.Should().BeTrue();
        }

        [TestCase("T@NK")]
        [TestCase("AAPL ")]
        [TestCase("AAP L")]
        [TestCase(" AAPL")]
        public void IsValid_InvalidSymbol_ReturnsFalse(string symbol)
        {
            bool result = symbol.IsValid();
            result.Should().BeFalse();
        }
    }
}
