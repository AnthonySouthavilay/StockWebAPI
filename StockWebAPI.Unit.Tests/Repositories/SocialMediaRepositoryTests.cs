using FluentAssertions;
using NUnit.Framework;
using StockWebAPI.Models.SocialMedia;
using StockWebAPI.Repository;
using StockWebAPI.Unit.Tests.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StockWebAPI.Unit.Tests.Repositories
{
    class SocialMediaRepositoryTests
    {
        private MockMessageHandler _mockMessageHandler;
        private HttpClient _httpClient;
        private SocialMediaRepository _socialMediaRepo;

        [Test]
        public async Task GetWallStreetBetsRecommendations_ReturnsListOfWSBInfo()
        {
            string mockResponse = "[{\"no_of_comments\":102,\"sentiment\":\"Bearish\",\"sentiment_score\":-0.001,\"ticker\":\"NEW\"},{\"no_of_comments\":55,\"sentiment\":\"Bearish\",\"sentiment_score\":-0.382,\"ticker\":\"CLOV\"},{\"no_of_comments\":54,\"sentiment\":\"Bullish\",\"sentiment_score\":0.04,\"ticker\":\"AAPL\"}]";
            _mockMessageHandler = new MockMessageHandler(mockResponse, HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _socialMediaRepo = new SocialMediaRepository(_httpClient);
            IEnumerable<WsbInfo> result = await _socialMediaRepo.GetWallStreetBetsRecommendationsAsync();
            result.Should().NotBeEmpty();
        }

        [Test]
        public async Task GetWallStreetBetsRecommendations_ChecksIfFirstEntryHasMostComments()
        {
            string mockResponse = "[{\"no_of_comments\":102,\"sentiment\":\"Bearish\",\"sentiment_score\":-0.001,\"ticker\":\"NEW\"},{\"no_of_comments\":55,\"sentiment\":\"Bearish\",\"sentiment_score\":-0.382,\"ticker\":\"CLOV\"},{\"no_of_comments\":54,\"sentiment\":\"Bullish\",\"sentiment_score\":0.04,\"ticker\":\"AAPL\"}]";
            _mockMessageHandler = new MockMessageHandler(mockResponse, HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _socialMediaRepo = new SocialMediaRepository(_httpClient);
            IEnumerable<WsbInfo> result = await _socialMediaRepo.GetWallStreetBetsRecommendationsAsync();
            WsbInfo firstEntry = result.ToArray()[0];
            WsbInfo secondEntry = result.ToArray()[1];
            firstEntry.No_of_comments.Should().BeGreaterThan(secondEntry.No_of_comments);
        }
    }
}
 