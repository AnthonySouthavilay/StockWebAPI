using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Extensions;
using NUnit.Framework;
using StockWebAPI.Models.Finnhub;
using StockWebAPI.Repository;
using StockWebAPI.Unit.Tests.TestHelpers;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockWebAPI.Unit.Tests.Repositories
{
    class FinnhubRepositoryTests
    {
        private FinnhubRepository _finnhubRepo;
        private HttpClient _httpClient;
        private MockMessageHandler _mockMessageHandler;

        [Test]
        public async Task GetCompanyNews_ValidSymbol_ReturnsCompanyNewsModelAsync()
        {
            string symbol = "TANK";
            string mockResponse = "[{\"category\":\"company\",\"datetime\":1588377600,\"headline\":\"WhatsApp Suddenly Gets Powerful\",\"id\":691320,\"image\":\"https:www.test.com\",\"related\":\"AAPL\",\"source\":\"https:www.forbes.com\",\"summary\":\"WhatsApp has been seriously boosted this week\u2014and from two very unlikely sources.\",\"url\":\"https:www.forbes.com\"},{\"category\":\"company\",\"datetime\":1588377600,\"headline\":\"2020 iPhone Alert: Apple\u2019s New Price Changes Revealed\",\"id\":691319,\"image\":\"https:thumbor.forbes.comimage\",\"related\":\"AAPL\",\"source\":\"https:www.forbes.com\",\"summary\":\"Apple's most exciting iPhone 12 change is a shock...\",\"url\":\"https:www.forbes.comsites\"}]";
            _mockMessageHandler = new MockMessageHandler(mockResponse, HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _finnhubRepo = new FinnhubRepository(_httpClient);
            FinnhubCompanyNews[] result = await _finnhubRepo.GetCompanyNewsAsync(symbol, new DateTime(), new DateTime());
            result.Should().NotBeNull();
        }

        [Test]
        public void GetCompanyNews_InvalidSymbol_ThrowsException()
        {
            string symbol = "AAP1";
            string mockResponse = "";
            _mockMessageHandler = new MockMessageHandler(mockResponse, HttpStatusCode.NotFound);
            _httpClient = new HttpClient(_mockMessageHandler);
            _finnhubRepo = new FinnhubRepository(_httpClient);
            Func<Task> act = async () => await Task.Run(() => _finnhubRepo.GetCompanyNewsAsync(symbol, new DateTime(), new DateTime()));
            act.Should().Throw<Exception>();            
        }

        [Test]
        public async Task GetCompanyNews_ValidSymbolProvideDate_ReturnsCompanyNewsModel()
        {
            string symbol = "TANK";
            DateTime startDate = new DateTime(2020, 04, 30);
            DateTime endDate = new DateTime(2020, 05, 01);
            string mockResponse = "[{\"category\":\"company\",\"datetime\":1588333111,\"headline\":\"WhatsApp Suddenly Gets Powerful\",\"id\":691320,\"image\":\"https:www.test.com\"," +
                "\"related\":\"AAPL\",\"source\":\"https:www.forbes.com\",\"summary\":\"WhatsApp has been seriously boosted this week\u2014and from two very unlikely sources.\",\"url\":" +
                "\"https:www.forbes.com\"},{\"category\":\"company\",\"datetime\":1588377600,\"headline\":\"2020 iPhone Alert: Apple\u2019s New Price Changes Revealed\",\"id\":691319,\"image" +
                "\":\"https:thumbor.forbes.comimage\",\"related\":\"AAPL\",\"source\":\"https:www.forbes.com\",\"summary\":\"Apple's most exciting iPhone 12 change is a shock...\",\"url\":" +
                "\"https:www.forbes.comsites\"}]";
            _mockMessageHandler = new MockMessageHandler(mockResponse, HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _finnhubRepo = new FinnhubRepository(_httpClient);
            FinnhubCompanyNews[] result = await _finnhubRepo.GetCompanyNewsAsync(symbol, startDate, endDate);
            using (new AssertionScope())
            {
                UnixTimestampToDateTime(result[0].datetime).Should().Be(1.May(2020));
                UnixTimestampToDateTime(result[1].datetime).Should().Be(2.May(2020));
            }
        }

        [Test]
        public async Task GetCompanyNews_ValidSymbolNoDate_ReturnsCurrentCompanyNews()
        {
            string symbol = "TNK";
            DateTime currentDate = new DateTime(2020, 12, 31);
            string mockResponse = "[{\"category\":\"company\",\"datetime\":1609391950,\"headline\":\"WhatsApp Suddenly Gets Powerful\",\"id\":691320,\"image\":\"https:www.test.com\"," +
                "\"related\":\"AAPL\",\"source\":\"https:www.forbes.com\",\"summary\":\"WhatsApp has been seriously boosted this week\u2014and from two very unlikely sources.\",\"url\":" +
                "\"https:www.forbes.com\"},{\"category\":\"company\",\"datetime\":1609391950,\"headline\":\"2020 iPhone Alert: Apple\u2019s New Price Changes Revealed\",\"id\":691319,\"image" +
                "\":\"https:thumbor.forbes.comimage\",\"related\":\"AAPL\",\"source\":\"https:www.forbes.com\",\"summary\":\"Apple's most exciting iPhone 12 change is a shock...\",\"url\":" +
                "\"https:www.forbes.comsites\"}]";
            _mockMessageHandler = new MockMessageHandler(mockResponse, HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _finnhubRepo = new FinnhubRepository(_httpClient);
            FinnhubCompanyNews[] result = await _finnhubRepo.GetCompanyNewsAsync(symbol);
            using (new AssertionScope())
            {
                UnixTimestampToDateTime(result[0].datetime).Should().Be(currentDate);
                UnixTimestampToDateTime(result[1].datetime).Should().Be(currentDate);
            }
        }

        [Test]
        public void GetCompanyNews_InvalidSymbolNoDates_ThrowsException()
        {
            string symbol = "AAP1";
            string mockResponse = "";
            _mockMessageHandler = new MockMessageHandler(mockResponse, HttpStatusCode.NotFound);
            _httpClient = new HttpClient(_mockMessageHandler);
            _finnhubRepo = new FinnhubRepository(_httpClient);
            Func<Task> act = async () => await Task.Run(() => _finnhubRepo.GetCompanyNewsAsync(symbol));
            act.Should().Throw<Exception>();
        }
        private static DateTime UnixTimestampToDateTime(double unixTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
            String shortDateTime = new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc).ToShortDateString();
            return Convert.ToDateTime(shortDateTime);
        }
    }
}
