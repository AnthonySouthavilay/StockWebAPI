using NUnit.Framework;
using StockWebAPI.Repository;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using StockWebAPI.Unit.Tests.TestHelpers;
using System.Net;
using FluentAssertions.Execution;
using StockWebAPI.Models.IEXCloud;
using StockWebAPI.Helpers;

namespace StockWebAPI.Unit.Tests.Repositories
{
    public class IEXRepositoryTests
    {
        private HttpClient _httpClient;
        private MockMessageHandler _mockMessageHandler;
        private IexRepository _iexRepo;

        [Test]
        public async Task GetCompanyInformation_ValidSymbol_ReturnsModel()
        {
            string testStockSymbol = "AAPL";
            string mockReturnedCompanyJSONResponse = "{\"symbol\":\"AAPL\",\"companyName\":\"Apple Inc.\",\"exchange\":\"NASDAQ\",\"industry\":\"Telecommunications Equipment\",\"website\":\"http://www.apple.com\"," +
                "\"description\":\"Apple, Inc. engages in the design, manufacture, and marketing of mobile communication, media devices, personal computers, and portable digital music players.\"," +
                "\"CEO\":\"Timothy Donald Cook\",\"securityName\":\"Apple Inc.\",\"issueType\":\"cs\",\"sector\":\"Electronic Technology\",\"primarySicCode\":3663,\"employees\":132000,\"tags\":[\"Electronic Technology\",\"Telecommunications Equipment\"]," +
                "\"address\":\"One Apple Park Way\",\"address2\":null,\"state\":\"CA\",\"city\":\"Cupertino\",\"zip\":\"95014-2083\",\"country\":\"US\",\"phone\":\"1.408.974.3123\"}";
            _mockMessageHandler = new MockMessageHandler(mockReturnedCompanyJSONResponse, HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _iexRepo = new IexRepository(_httpClient);
            CompanyProfile result = await _iexRepo.GetCompanyInfoAsync(testStockSymbol);
            using (new AssertionScope())
            {
                result.symbol.Should().Be(testStockSymbol);
                result.employees.Should().Be(132000);
                result.Should().NotBeNull();
            }
        }

        [Test]
        public Task GetCompanyInformation_UnknownSymbol_ThrowsApiException()
        {
            string testUnknownSymbol = "G00G";
            string mockResponse = "Unknown symbol";
            _mockMessageHandler = new MockMessageHandler(mockResponse, HttpStatusCode.NotFound);
            _httpClient = new HttpClient(_mockMessageHandler);
            _iexRepo = new IexRepository(_httpClient);
            Func<Task> result = async () => { await _iexRepo.GetCompanyInfoAsync(testUnknownSymbol); };
            result.Should().Throw<ApiException>();
            return Task.CompletedTask;
        }

        [Test]
        public async Task GetQuote_ValidSymbol_ReturnsQuoteModel()
        {
            string symbol = "TANK";
            string mockResponse = "{\"symbol\":\"TANK\",\"companyName\":\"Apple Inc\",\"primaryExchange\":\"NASDAQNGS (GLOBAL SELECT MARKET)\",\"calculationPrice\":\"previousclose\",\"open\":null,\"openTime\":null,\"openSource\":\"official\",\"close\":null,\"closeTime\":null,\"closeSource\":\"official\",\"high\":null,\"highTime\":null,\"highSource\":null,\"low\":null,\"lowTime\":null,\"lowSource\":null,\"latestPrice\":131.97,\"latestSource\":\"Previous close\",\"latestTime\":\"December 24, 2020\",\"latestUpdate\":1608786000000,\"latestVolume\":null,\"iexRealtimePrice\":null,\"iexRealtimeSize\":null,\"iexLastUpdated\":null,\"delayedPrice\":null,\"delayedPriceTime\":null,\"oddLotDelayedPrice\":null,\"oddLotDelayedPriceTime\":null,\"extendedPrice\":null,\"extendedChange\":null,\"extendedChangePercent\":null,\"extendedPriceTime\":null,\"previousClose\":131.97,\"previousVolume\":54930064,\"change\":0,\"changePercent\":0,\"volume\":null,\"iexMarketPercent\":null,\"iexVolume\":null,\"avgTotalVolume\":104914724,\"iexBidPrice\":null,\"iexBidSize\":null,\"iexAskPrice\":null,\"iexAskSize\":null,\"iexOpen\":132.025,\"iexOpenTime\":1608832797593,\"iexClose\":132.025,\"iexCloseTime\":1608832797593,\"marketCap\":2243727809940,\"peRatio\":40.23,\"week52High\":133.95,\"week52Low\":55.74,\"ytdChange\":0.8131757160580841,\"lastTradeTime\":1608832798599,\"isUSMarketOpen\":false}";
            _mockMessageHandler = new MockMessageHandler(mockResponse, HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _iexRepo = new IexRepository(_httpClient);
            IexQuote result = await _iexRepo.GetQuoteAsync(symbol);
            using (new AssertionScope())
            {
                result.Should().NotBeNull();
                result.symbol.Should().Be(symbol);
            }
        }
    }
}