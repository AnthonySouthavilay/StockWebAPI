using NUnit.Framework;
using StockWebAPI.Models;
using StockWebAPI.Repository;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using StockWebAPI.Unit.Tests.TestHelpers;

namespace StockWebAPI.Unit.Tests.Repositories
{
    public class IEXRepositoryTests
    {
        private HttpClient httpClient;
        private MockMessageHandler mockMessageHandler;
        private IEXRepository iexRepo;

        [Test]
        public async Task GetCompanyInformation_ValidSymbol_ReturnsModelAsync()
        {
            string testStockSymbol = "AAPL";
            string mockReturnedCompanyJSONResponse = "{\"symbol\":\"AAPL\",\"companyName\":\"Apple Inc.\",\"exchange\":\"NASDAQ\",\"industry\":\"Telecommunications Equipment\",\"website\":\"http://www.apple.com\"," +
                "\"description\":\"Apple, Inc. engages in the design, manufacture, and marketing of mobile communication, media devices, personal computers, and portable digital music players.\"," +
                "\"CEO\":\"Timothy Donald Cook\",\"securityName\":\"Apple Inc.\",\"issueType\":\"cs\",\"sector\":\"Electronic Technology\",\"primarySicCode\":3663,\"employees\":132000,\"tags\":[\"Electronic Technology\",\"Telecommunications Equipment\"]," +
                "\"address\":\"One Apple Park Way\",\"address2\":null,\"state\":\"CA\",\"city\":\"Cupertino\",\"zip\":\"95014-2083\",\"country\":\"US\",\"phone\":\"1.408.974.3123\"}";
            mockMessageHandler = new MockMessageHandler(mockReturnedCompanyJSONResponse, System.Net.HttpStatusCode.OK);
            httpClient = new HttpClient(mockMessageHandler);
            iexRepo = new IEXRepository(httpClient);
            var result = await iexRepo.GetCompanyInfoAsync(testStockSymbol);
            result.Should().NotBeNull();
        }
    }
}