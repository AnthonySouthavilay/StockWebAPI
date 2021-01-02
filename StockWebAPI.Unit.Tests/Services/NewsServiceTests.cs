using NUnit.Framework;
using StockWebAPI.Service;
using StockWebAPI.Unit.Tests.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using StockWebAPI.ViewModels;
using FluentAssertions;
using StockWebAPI.Models.Finnhub;

namespace StockWebAPI.Unit.Tests.Services
{
    public class NewsServiceTests
    {
        private HttpClient _httpClient;
        private MockMessageHandler _mockMessageHandler;
        private NewsService _sut;

        [Test]
        public async Task GetCompanyNews_ValidSymbol_ReturnsArrayOfViewModels()
        {
            string symbol = "TANK";
            string mockResponse = "[{\"category\":\"company\",\"datetime\":1588377600,\"headline\":\"WhatsApp Suddenly Gets Powerful\",\"id\":691320,\"image\":\"https:www.test.com\",\"related\":\"AAPL\"," +
                "\"source\":\"https:www.forbes.com\",\"summary\":\"WhatsApp has been seriously boosted this week\u2014and from two very unlikely sources.\",\"url\":\"https:www.forbes.comsites\"}," +
                "{\"category\":\"company\",\"datetime\":1588377600,\"headline\":\"2020 iPhone Alert: Apple\u2019s New Price Changes Revealed\",\"id\":691319,\"image\":\"https:thumbor.forbes.comimage\"," +
                "\"related\":\"AAPL\",\"source\":\"https:www.forbes.com\",\"summary\":\"Apple's most exciting iPhone 12 change is a shock...\",\"url\":\"https:www.forbes.comsites\"}]";
            _mockMessageHandler = new MockMessageHandler(mockResponse, HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _sut = new NewsService(_httpClient);
            CompanyNewsViewModel[] result = await _sut.GetCompanyNewsAsync(symbol);
            result.Should().NotBeNull();
        }

        [TestCase("T@NK")]
        [TestCase("T@N3K")]
        [TestCase("@!NK")]
        [TestCase(" TANK")]
        [TestCase("TN3K ")]
        [TestCase(null)]
        [TestCase("")]
        public void GetCompanyNews_InvalidSymbol_ReturnsArgumentException(string invalidSymbol)
        {
            _mockMessageHandler = new MockMessageHandler("", HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _sut = new NewsService(_httpClient);
            Func<Task> result = async () => { await _sut.GetCompanyNewsAsync(invalidSymbol); };
            result.Should().Throw<ArgumentException>();
        }
    }
}
