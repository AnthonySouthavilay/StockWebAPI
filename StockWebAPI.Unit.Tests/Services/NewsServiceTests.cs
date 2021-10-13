using NUnit.Framework;
using StockWebAPI.Service;
using StockWebAPI.Unit.Tests.TestHelpers;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using StockWebAPI.ViewModels;
using FluentAssertions;
using FluentAssertions.Extensions;
using FluentAssertions.Execution;

namespace StockWebAPI.Unit.Tests.Services
{
    public class NewsServiceTests
    {
        private HttpClient _httpClient;
        private MockMessageHandler _mockMessageHandler;
        private NewsService _sut;
        private const string mockResponseDateRange = "[{\"category\":\"company\",\"datetime\":1609607400,\"headline\":\"Apple removes 39,000 game apps from China store to meet deadline\",\"id\":62102184," +
                "\"image\":\"httpsapicms.thestar.com.myuploadsimages/20210102993826.jpg\",\"related\":\"AAPL\",\"source\":\"https:www.thestar.com.my\"," +
                "\"summary\":\"Apple removed 39,000 game apps on its China store on Dec 31, the biggest removal ever in a single day, as it set year-end as deadline for all game publishers to obtain a licence.\"," +
                "\"url\":\"https:www.thestar.com.myechtech-news20210102apple-removes-39000-game-apps-from-china-store-to-meet-deadline\"},{\"category\":\"company\",\"datetime\":1609588440," +
                "\"headline\":\"Bold Prediction: General Motors Is the Next Apple Stock for Investors\",\"id\":62102277,\"image\":\"https:www.nasdaq.comsitesacquia.prodfiles2019-050902-Q19%20Total%20Markets%20photos%20and%20gif_CC8.jpg?897272992\"," +
                "\"related\":\"AAPL\",\"source\":\"Nasdaq\",\"summary\":\"Two decades ago, Apple (NASDAQ: AAPL) was a floundering technology company without much in the way of compelling devices. The iPod wouldn't be introduced until 2001, d t\",\"url\":\"https:www.nasdaq.comarticlesbold-prediction%3A-general-motors-is-the-next-apple-stock-for-investors-2021-01-02\"}]";


        [Test]
        public async Task GetCurrentCompanyNewsAsync_ValidSymbol_ReturnsArrayOfViewModels()
        {
            string symbol = "TANK";
            string mockResponse = "[{\"category\":\"company\",\"datetime\":1588377600,\"headline\":\"WhatsApp Suddenly Gets Powerful\",\"id\":691320,\"image\":\"https:www.test.com\",\"related\":\"AAPL\"," +
                "\"source\":\"https:www.forbes.com\",\"summary\":\"WhatsApp has been seriously boosted this week\u2014and from two very unlikely sources.\",\"url\":\"https:www.forbes.comsites\"}," +
                "{\"category\":\"company\",\"datetime\":1588377600,\"headline\":\"2020 iPhone Alert: Apple\u2019s New Price Changes Revealed\",\"id\":691319,\"image\":\"https:thumbor.forbes.comimage\"," +
                "\"related\":\"AAPL\",\"source\":\"https:www.forbes.com\",\"summary\":\"Apple's most exciting iPhone 12 change is a shock...\",\"url\":\"https:www.forbes.comsites\"}]";
            _mockMessageHandler = new MockMessageHandler(mockResponse, HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _sut = new NewsService(_httpClient);
            CompanyNewsViewModel[] result = await _sut.GetCurrentCompanyNewsAsync(symbol);
            result.Should().NotBeNull();
        }

        [TestCase("T@NK")]
        [TestCase("T@N3K")]
        [TestCase("@!NK")]
        [TestCase(" TANK")]
        [TestCase("TN3K ")]
        [TestCase(null)]
        [TestCase("")]
        public void GetCurrentCompanyNewsAsync_InvalidSymbol_ReturnsArgumentException(string invalidSymbol)
        {
            _mockMessageHandler = new MockMessageHandler("", HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _sut = new NewsService(_httpClient);
            Func<Task> result = async () => { await _sut.GetCurrentCompanyNewsAsync(invalidSymbol); };
            result.Should().Throw<ArgumentException>();
        }

        [Test]
        public async Task GetCompanyNewsByDateRangeAsync_ValidSymbolWithDashesInDates_ReturnsViewModel()
        {
            string symbol = "TANK";
            _mockMessageHandler = new MockMessageHandler(mockResponseDateRange, HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _sut = new NewsService(_httpClient);
            CompanyNewsViewModel[] result = await _sut.GetCompanyNewsByDateRangeAsync(symbol, "2021-01-01", "2021-01-02");
            result.Should().NotBeNull();
        }

        [Test]
        public async Task GetCompanyNewsByDateRangeAsync_ValidSymbolWithSlashesInDates_ReturnsViewModel()
        {
            string symbol = "TANK";
            _mockMessageHandler = new MockMessageHandler(mockResponseDateRange, HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _sut = new NewsService(_httpClient);
            CompanyNewsViewModel[] result = await _sut.GetCompanyNewsByDateRangeAsync(symbol, "01/01/2021", "01/02/2021");
            result.Should().NotBeNull();
        }

        [Test]
        public async Task GetCompanyNewsByDateRangeAsync_ValidSymbolWithEncodedForwardSlashesInDate_ReturnsViewModel()
        {
            string symbol = "TANK";
            _mockMessageHandler = new MockMessageHandler(mockResponseDateRange, HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _sut = new NewsService(_httpClient);
            CompanyNewsViewModel[] result = await _sut.GetCompanyNewsByDateRangeAsync(symbol, "6%2F25%2F2019", "6%2F25%2F2019");
            result.Should().NotBeNull();
        }

    }
}
