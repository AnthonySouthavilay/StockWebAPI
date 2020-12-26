using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using StockWebAPI.Service;
using StockWebAPI.Unit.Tests.TestHelpers;
using StockWebAPI.ViewModels;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockWebAPI.Unit.Tests.Services
{
    public class CompanyServiceTests
    {
        private HttpClient _httpClient;
        private MockMessageHandler _mockMessageHandler;
        private CompanyService _sut;

        [Test]
        public async Task GetCompanyProfile_ValidSymbol_ReturnsCompanyProfileViewModelAsync()
        {
            string symbol = "AAPL";
            // mock json response
            string mockReturnedCompanyJSONResponse = "{\"symbol\":\"AAPL\",\"companyName\":\"Apple Inc.\",\"exchange\":\"NASDAQ\",\"industry\":\"Telecommunications Equipment\",\"website\":\"http://www.apple.com\"," +
            "\"description\":\"Apple, Inc. engages in the design, manufacture, and marketing of mobile communication, media devices, personal computers, and portable digital music players.\"," +
            "\"CEO\":\"Timothy Donald Cook\",\"securityName\":\"Apple Inc.\",\"issueType\":\"cs\",\"sector\":\"Electronic Technology\",\"primarySicCode\":3663,\"employees\":132000,\"tags\":[\"Electronic Technology\",\"Telecommunications Equipment\"]," +
            "\"address\":\"One Apple Park Way\",\"address2\":null,\"state\":\"CA\",\"city\":\"Cupertino\",\"zip\":\"95014-2083\",\"country\":\"US\",\"phone\":\"1.408.974.3123\"}";

            _mockMessageHandler = new MockMessageHandler(mockReturnedCompanyJSONResponse, HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _sut = new CompanyService(_httpClient);
            CompanyProfileViewModel companyProfile = await _sut.GetCompanyProfileAsync(symbol);
            companyProfile.Should().NotBeNull();
        }

        [TestCase("T@NK")]
        [TestCase("T@N3K")]
        [TestCase("@!NK")]
        public void GetCompanyProfile_InvalidSymbol_ReturnsArgumentException(string invalidSymbol)
        {
            _mockMessageHandler = new MockMessageHandler("", HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _sut = new CompanyService(_httpClient);
            Func<Task> result = async () => { await _sut.GetCompanyProfileAsync(invalidSymbol); };
            result.Should().Throw<ArgumentException>();
        }

        [TestCase(" TANK")]
        [TestCase("TN3K ")]
        [TestCase(null)]
        public void GetCompanyProfile_SymbolHasWhiteSpaceOrIsNull_ReturnsArgumentException(string symbol)
        {
            _mockMessageHandler = new MockMessageHandler("", HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _sut = new CompanyService(_httpClient);
            Func<Task> result = async () => { await _sut.GetCompanyProfileAsync(symbol); };
            result.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetCompanyProfile_EmptySymbol_ReturnsArgumentException()
        {
            _mockMessageHandler = new MockMessageHandler("", HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _sut = new CompanyService(_httpClient);
            Func<Task> result = async () => { await _sut.GetCompanyProfileAsync(""); };
            result.Should().Throw<ArgumentException>();
        }

        [Test]
        public async Task GetCompanySummary_ValidSymbol_ReturnsCompanySummaryViewModelAsync()
        {
            string symbol = "AAPL";
            HttpClient client = new HttpClient();
            _sut = new CompanyService(client);
            CompanySummaryViewModel result = await _sut.GetCompanySummaryAsync(symbol);
            using(new AssertionScope())
            {
                result.Should().NotBeNull();
                result.Symbol.Should().Be(symbol);
            }
        }
    }
}
