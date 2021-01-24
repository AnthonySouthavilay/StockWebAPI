using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using StockWebAPI.Helpers;
using System;

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
        
        [Test]
        public void GetBaseUrl_ValidKey_ReturnsBaseUrl()
        {
            string apiKey = "Iex";
            string result = apiKey.GetBaseUrl();
            result.Should().NotBeNullOrEmpty(); 
        }

        [Test]
        public void GetBaseUrl_InvalidKey_ReturnsArgumentException()
        {
            string apiKey = "invalid key";
            Action result = () => apiKey.GetBaseUrl();
            result.Should().Throw<ArgumentException>();
        }
    }
}
