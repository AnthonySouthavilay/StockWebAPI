using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using StockWebAPI.Helpers;
using StockWebAPI.Models.AlphaVantage;
using StockWebAPI.Repository;
using StockWebAPI.Unit.Tests.TestHelpers;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockWebAPI.Unit.Tests.Repositories
{
    class AlphaVantageRespositoryTests
    {
        private MockMessageHandler _mockMessageHandler;
        private HttpClient _httpClient;
        private AlphaVantageRepository _alphaVantageRepo;

        [Test]
        public async Task GetKeyInformation_ValidSymbol_ReturnsModelAsync()
        {
            string symbol = "IBM";
            string mockJSONResponse = "{\"Symbol\":\"IBM\",\"AssetType\":\"Common Stock\",\"Name\":\"International Business Machines Corporation\",\"Description\":\"International Business Machines Corporation provides integrated solutions and services " +
                "worldwide. Its Cloud & Cognitive Software segment offers software for vertical and domain-specific solutions in health, financial services, and Internet of Things (IoT), weather, and security software and services application areas; and " +
                "customer information control system and storage, and analytics and integration software solutions to support client mission critical on-premise workloads in banking, airline, and retail industries. It also offers middleware and data " +
                "platform software, including Red Hat that enables the operation of clients' hybrid multi-cloud environments; and Cloud Paks, WebSphere distributed, and analytics platform software, such as DB2 distributed, information integration, and " +
                "enterprise content management, as well as IoT, Blockchain and AI Watson platforms. The company's Global Business Services segment offers business consulting services; system integration, application management, maintenance, and " +
                "support services for packaged software; finance, procurement, talent and engagement, and industry-specific business process outsourcing services; and IT infrastructure and platform services. Its Global Technology Services segment provides " +
                "project, managed, outsourcing, and cloud-delivered services for enterprise IT infrastructure environments; and IT infrastructure support services. The company's Systems segment offers servers for businesses, cloud service providers, and " +
                "scientific computing organizations; data storage products and solutions; and OS, an enterprise operating system, as well as Linux. Its Global Financing segment provides lease, installment payment, loan financing, short-term working " +
                "capital financing, and remanufacturing and remarketing services. The company was formerly known as Computing-Tabulating-Recording Co. The company was founded in 1911 and is headquartered in Armonk, New York.\"," +
                "\"Exchange\":\"NYSE\",\"Currency\":\"USD\",\"Country\":\"USA\",\"Sector\":\"Technology\",\"Industry\":\"Information Technology Services\",\"Address\":\"One New Orchard Road, Armonk, NY, United States, 10504\"," +
                "\"FullTimeEmployees\":\"352600\",\"FiscalYearEnd\":\"December\",\"LatestQuarter\":\"2020-09-30\",\"MarketCapitalization\":\"110183653376\",\"EBITDA\":\"15690000384\",\"PERatio\":\"14.0151\",\"PEGRatio\":\"9.3438\"," +
                "\"BookValue\":\"23.801\",\"DividendPerShare\":\"6.52\",\"DividendYield\":\"0.0518\",\"EPS\":\"8.823\",\"RevenuePerShareTTM\":\"84.402\",\"ProfitMargin\":\"0.1053\",\"OperatingMarginTTM\":\"0.1205\",\"ReturnOnAssetsTTM\":\"0.0372\"," +
                "\"ReturnOnEquityTTM\":\"0.401\",\"RevenueTTM\":\"75030003712\",\"GrossProfitTTM\":\"36489000000\",\"DilutedEPSTTM\":\"8.823\",\"QuarterlyEarningsGrowthYOY\":\"0.011\",\"QuarterlyRevenueGrowthYOY\":\"-0.026\",\"AnalystTargetPrice\":" +
                "\"137.13\",\"TrailingPE\":\"14.0151\",\"ForwardPE\":\"10.7411\",\"PriceToSalesRatioTTM\":\"1.5017\",\"PriceToBookRatio\":\"5.2876\",\"EVToRevenue\":\"2.2249\",\"EVToEBITDA\":\"10.9021\",\"Beta\":\"1.2399\",\"52WeekHigh\":\"150.8394\"," +
                "\"52WeekLow\":\"86.9458\",\"50DayMovingAverage\":\"120.8034\",\"200DayMovingAverage\":\"122.0729\",\"SharesOutstanding\":\"891057024\",\"SharesFloat\":\"889720530\",\"SharesShort\":\"22028993\",\"SharesShortPriorMonth\":\"24559462\"," +
                "\"ShortRatio\":\"4.14\",\"ShortPercentOutstanding\":\"0.02\",\"ShortPercentFloat\":\"0.0247\",\"PercentInsiders\":\"0.116\",\"PercentInstitutions\":\"58.594\",\"ForwardAnnualDividendRate\":\"6.52\",\"ForwardAnnualDividendYield\":" +
                "\"0.0518\",\"PayoutRatio\":\"0.5756\",\"DividendDate\":\"2020-12-10\",\"ExDividendDate\":\"2020-11-09\",\"LastSplitFactor\":\"2:1\",\"LastSplitDate\":\"1999-05-27\"}";
            _mockMessageHandler = new MockMessageHandler(mockJSONResponse, HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            HttpClient liveHttp = new HttpClient();
            _alphaVantageRepo = new AlphaVantageRepository(liveHttp);
            AlphaVantageCompanyKeyStats result = await _alphaVantageRepo.GetKeyInformationAsync(symbol);
            using (new AssertionScope())
            {
                result.Symbol.Should().Be(symbol);
                result.Should().NotBeNull();
            }
        }

        [Test]
        public Task GetKeyInformation_UnknownSymbol_ThrowsApiException()
        {
            string testUnknownSymbol = "G00G";
            string mockResponse = "Unknown symbol";
            _mockMessageHandler = new MockMessageHandler(mockResponse, HttpStatusCode.NotFound);
            _httpClient = new HttpClient(_mockMessageHandler);
            _alphaVantageRepo = new AlphaVantageRepository(_httpClient);
            Func<Task> result = async () => { await _alphaVantageRepo.GetKeyInformationAsync(testUnknownSymbol); };
            result.Should().Throw<ApiException>();
            return Task.CompletedTask;
        }

        [Test]
        public async Task GetQuote_ValidSymbol_ReturnsQuoteModel()
        {
            string symbol = "TANK";
            string mockResponse = "{\"Global Quote\":{\"01. symbol\":\"TANK\",\"02. open\":\"125.0000\",\"03. high\":\"125.1000\",\"04. low\":\"124.2100\",\"05. price\":\"124.6900\",\"06. volume\":\"1761122\",\"07. latest trading day\":\"2020-12-24\",\"08. previous close\":\"123.9000\",\"09. change\":\"0.7900\",\"10. change percent\":\"0.6376%\"}}";
            _mockMessageHandler = new MockMessageHandler(mockResponse, HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _alphaVantageRepo = new AlphaVantageRepository(_httpClient);
            AlphaVantageQuote result = await _alphaVantageRepo.GetQuote(symbol);
            using (new AssertionScope())
            {
                result.Symbol.Should().Be(symbol);
                result.Should().NotBeNull();
            }
        }

        [Test]
        public Task GetQuote_InvalidSymbol_ThrowsApiException()
        {
            string testUnknownSymbol = "G00G";
            string mockResponse = "Unknown symbol";
            _mockMessageHandler = new MockMessageHandler(mockResponse, HttpStatusCode.NotFound);
            _httpClient = new HttpClient(_mockMessageHandler);
            _alphaVantageRepo = new AlphaVantageRepository(_httpClient);
            Func<Task> result = async () => { await _alphaVantageRepo.GetQuote(testUnknownSymbol); };
            result.Should().Throw<ApiException>();
            return Task.CompletedTask;
        }
    }
}