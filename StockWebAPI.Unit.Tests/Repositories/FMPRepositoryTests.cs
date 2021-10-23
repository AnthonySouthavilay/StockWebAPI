using FluentAssertions;
using NUnit.Framework;
using StockWebAPI.Models.FinancialModelingPrep;
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
    class FMPRepositoryTests
    {
        private FmpRepository _fmpRepo;
        private HttpClient _httpClient;
        private MockMessageHandler _mockMessageHandler;

        [Test]
        public async Task GetCompanyRating_ValidSymbol_ReturnsFMPCompanyRatingsAsync()
        {
            string symbol = "AAPL";
            FMPCompanyRating expectedResult = new()
            {
                Symbol = "AAPL",
                Date = "2021-10-15",
                Rating = "S",
                RatingScore = 5,
                RatingRecommendation = "Strong Buy",
                RatingDetailsDCFScore = 5,
                RatingDetailsDCFRecommendation = "Strong Buy",
                RatingDetailsROEScore = 5,
                RatingDetailsROERecommendation = "Strong Buy",
                RatingDetailsROAScore = 3,
                RatingDetailsROARecommendation = "Neutral",
                RatingDetailsDEScore = 5,
                RatingDetailsDERecommendation ="Strong Buy",
                RatingDetailsPEScore = 5,
                RatingDetailsPERecommendation ="Strong Buy",
                RatingDetailsPBScore = 5,
                RatingDetailsPBRecommendation = "Strong Buy"
            };
            string mockResponse = "[{\"symbol\":\"AAPL\",\"date\":\"2021-10-15\",\"rating\":\"S\",\"ratingScore\":5,\"ratingRecommendation\":\"Strong Buy\",\"ratingDetailsDCFScore\":5,\"ratingDetailsDCFRecommendation\":\"Strong Buy\",\"ratingDetailsROEScore\":5,\"ratingDetailsROERecommendation\":\"Strong Buy\",\"ratingDetailsROAScore\":3,\"ratingDetailsROARecommendation\":\"Neutral\",\"ratingDetailsDEScore\":5,\"ratingDetailsDERecommendation\":\"Strong Buy\",\"ratingDetailsPEScore\":5,\"ratingDetailsPERecommendation\":\"Strong Buy\",\"ratingDetailsPBScore\":5,\"ratingDetailsPBRecommendation\":\"Strong Buy\"}]";
            _mockMessageHandler = new MockMessageHandler(mockResponse, HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _fmpRepo = new FmpRepository(_httpClient);
            FMPCompanyRating[] actualResult = await _fmpRepo.GetCompanyRatingAsync(symbol);
            actualResult[0].Should().BeEquivalentTo(expectedResult);
        }
    }
}
