using FluentAssertions;
using NUnit.Framework;
using StockWebAPI.Models.GNews;
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
    class GNewsRepositoryTests
    {
        private GNewsRepository _gNewsRepository;
        private HttpClient _httpClient;
        private MockMessageHandler _mockMessageHandler;

        [Test]
        public async Task GetGNewsAsync_ValidSymbol_ReturnsCompanyNewsViewModelAsync()
        {
            string validSymbol = "GOOD";
            string mockResponse = "{\"totalArticles\":103,\"articles\":[{\"title\":\"Warren Buffett praises AAPL stock in annual letter\",\"description\":\"Warren Buffetts Berkshire Hathaway published its annual letter this morning, and it includes details on its AAPL holdings.\",\"content\":\"Warren Buffetts Berkshire Hathaway Inc. published its highly-anticipated annual letter this morning, offer details on the conglomerates investments in 2020. In this year s letter, Buffett touted that Apple ranks as Berkshire s biggest common stock ... [2562 chars]\",\"url\":\"https:  9to5mac.com 2021 02 27 warren-buffett-aapl-stake-value \",\"image\":\"https:  i2.wp.com 9to5mac.com wp-content uploads sites 6 2018 08 Warre\",\"publishedAt\":\"2021-02-27T19:19:39Z\",\"source\":{\"name\":\"9to5Mac\",\"url\":\"https:  9to5mac.com\"}},{\"title\":\"6 Key Questions Apple Answered At The Annual Shareholder Event\",\"description\":\"Shares of Apple Inc (NASDAQ:AAPL) has taken a beating in the recent market sell-off, spearheaded by tech stocks. At its annual shareholder event T...\",\"content\":\"Shares of Apple Inc (NASDAQ:AAPL) has taken a beating in the recent market sell-off, spearheaded by tech stocks. At its annual shareholder event Tuesday, the company assuaged concerns by answering a slew of questions in the Q&A sessionre are the ... [2278 chars]\",\"url\":\"https:  markets.businessinsider.com news stocks 6-key-questions-apple-answered-at-the-annual-shareholder-event-1030120836\",\"image\":\"https:  cdn.benzinga.com files imagecache 1024x768xUP images story 2012 appletv_timcook_2_2_12.png\",\"publishedAt\":\"2021-02-25T07:07:56Z\",\"source\":{\"name\":\"Business Insider\",\"url\":\"https:  markets.businessinsider.com\"}}]}";
            _mockMessageHandler = new MockMessageHandler(mockResponse, HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _gNewsRepository = new GNewsRepository(_httpClient);
            var result = await _gNewsRepository.GetGNewsAsync(validSymbol);
            result.Should().NotBeEmpty();
        }
    }
}
