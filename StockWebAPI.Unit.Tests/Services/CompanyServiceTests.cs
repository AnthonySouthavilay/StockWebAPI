using FluentAssertions;
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
        private HttpClient httpClient;
        private MockMessageHandler mockMessageHandler;
        private CompanyService sut;

        [Test]
        public async Task GetCompanyProfile_ValidSymbol_ReturnsCompanyProfileViewModelAsync()
        {
            string symbol = "AAPL";
            // mock json response
            string mockReturnedCompanyJSONResponse = "{\"symbol\":\"AAPL\",\"companyName\":\"Apple Inc.\",\"exchange\":\"NASDAQ\",\"industry\":\"Telecommunications Equipment\",\"website\":\"http://www.apple.com\"," +
    "\"description\":\"Apple, Inc. engages in the design, manufacture, and marketing of mobile communication, media devices, personal computers, and portable digital music players.\"," +
    "\"CEO\":\"Timothy Donald Cook\",\"securityName\":\"Apple Inc.\",\"issueType\":\"cs\",\"sector\":\"Electronic Technology\",\"primarySicCode\":3663,\"employees\":132000,\"tags\":[\"Electronic Technology\",\"Telecommunications Equipment\"]," +
    "\"address\":\"One Apple Park Way\",\"address2\":null,\"state\":\"CA\",\"city\":\"Cupertino\",\"zip\":\"95014-2083\",\"country\":\"US\",\"phone\":\"1.408.974.3123\"}";

            mockMessageHandler = new MockMessageHandler(mockReturnedCompanyJSONResponse, HttpStatusCode.OK);
            httpClient = new HttpClient(mockMessageHandler);
            sut = new CompanyService(httpClient);
            CompanyProfileViewModel companyProfile = await sut.GetCompanyProfileAsync(symbol);
            companyProfile.Should().NotBeNull();
        }

        [TestCase("T@NK")]
        [TestCase("T@N3K")]
        [TestCase("@!NK")]
        public void GetCompanyProfile_InvalidSymbol_ReturnsArgumentException(string invalidSymbol)
        {
            mockMessageHandler = new MockMessageHandler("", HttpStatusCode.OK);
            httpClient = new HttpClient(mockMessageHandler);
            sut = new CompanyService(httpClient);
            Func<Task> result = async () => { await sut.GetCompanyProfileAsync(invalidSymbol); };
            result.Should().Throw<ArgumentException>();
        }

        [TestCase(" TANK")]
        [TestCase("TN3K ")]
        [TestCase(null)]
        public void GetCompanyProfile_SymbolHasWhiteSpaceOrIsNull_ReturnsArgumentException(string symbol)
        {
            mockMessageHandler = new MockMessageHandler("", HttpStatusCode.OK);
            httpClient = new HttpClient(mockMessageHandler);
            sut = new CompanyService(httpClient);
            Func<Task> result = async () => { await sut.GetCompanyProfileAsync(symbol); };
            result.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetCompanyProfile_EmptySymbol_ReturnsArgumentException()
        {
            mockMessageHandler = new MockMessageHandler("", HttpStatusCode.OK);
            httpClient = new HttpClient(mockMessageHandler);
            sut = new CompanyService(httpClient);
            Func<Task> result = async () => { await sut.GetCompanyProfileAsync(""); };
            result.Should().Throw<ArgumentException>();
        }
    }
}
