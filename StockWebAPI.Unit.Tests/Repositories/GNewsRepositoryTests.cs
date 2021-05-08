using FluentAssertions;
using NUnit.Framework;
using StockWebAPI.Helpers;
using StockWebAPI.Repository;
using StockWebAPI.Unit.Tests.TestHelpers;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockWebAPI.Unit.Tests.Repositories
{
    class GNewsRepositoryTests
    {
        private GNewsRepository _gNewsRepository;
        private HttpClient _httpClient;
        private MockMessageHandler _mockMessageHandler;
        private const string validSymbol = "GOOG";
        private const string invalidSymbol = "G00G";

        [Test]
        public async Task GetGNewsAsync_ValidSymbol_ReturnsIEnumerableOfCompanyNewsArticles()
        {
            string mockResponse = "{\"totalArticles\":103,\"articles\":[{\"title\":\"Warren Buffett praises AAPL stock in annual letter\",\"description\":\"Warren Buffetts Berkshire Hathaway published its annual letter this morning, and it includes details on its AAPL holdings.\",\"content\":\"Warren Buffetts Berkshire Hathaway Inc. published its highly-anticipated annual letter this morning, offer details on the conglomerates investments in 2020. In this year s letter, Buffett touted that Apple ranks as Berkshire s biggest common stock ... [2562 chars]\",\"url\":\"https:  9to5mac.com 2021 02 27 warren-buffett-aapl-stake-value \",\"image\":\"https:  i2.wp.com 9to5mac.com wp-content uploads sites 6 2018 08 Warre\",\"publishedAt\":\"2021-02-27T19:19:39Z\",\"source\":{\"name\":\"9to5Mac\",\"url\":\"https:  9to5mac.com\"}},{\"title\":\"6 Key Questions Apple Answered At The Annual Shareholder Event\",\"description\":\"Shares of Apple Inc (NASDAQ:AAPL) has taken a beating in the recent market sell-off, spearheaded by tech stocks. At its annual shareholder event T...\",\"content\":\"Shares of Apple Inc (NASDAQ:AAPL) has taken a beating in the recent market sell-off, spearheaded by tech stocks. At its annual shareholder event Tuesday, the company assuaged concerns by answering a slew of questions in the Q&A sessionre are the ... [2278 chars]\",\"url\":\"https:  markets.businessinsider.com news stocks 6-key-questions-apple-answered-at-the-annual-shareholder-event-1030120836\",\"image\":\"https:  cdn.benzinga.com files imagecache 1024x768xUP images story 2012 appletv_timcook_2_2_12.png\",\"publishedAt\":\"2021-02-25T07:07:56Z\",\"source\":{\"name\":\"Business Insider\",\"url\":\"https:  markets.businessinsider.com\"}}]}";
            _mockMessageHandler = new MockMessageHandler(mockResponse, HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _gNewsRepository = new GNewsRepository(_httpClient);
            var result = await _gNewsRepository.GetGNewsAsync(validSymbol);
            result.Should().NotBeEmpty();
        }

        [Test]
        public void GetGNewsAsync_InvalidSymbol_ThrowsApiException()
        {
            _mockMessageHandler = new MockMessageHandler("", HttpStatusCode.NotFound);
            _httpClient = new HttpClient(_mockMessageHandler);
            _gNewsRepository = new GNewsRepository(_httpClient);
            Func<Task> act = async () => await Task.Run(() => _gNewsRepository.GetGNewsAsync(invalidSymbol));
            act.Should().Throw<ApiException>();
        }

        [Test]
        public async Task GetGNewsAsync_ValidSymbolWithStartAndEndDate_ReturnsIEnumerableOfCompanyNewsArticles()
        {
            string mockResponse = "{\"totalArticles\":2,\"articles\":[{\"title\":\"Warren Buffett praises AAPL stock in annual letter\",\"description\":\"Warren Buffetts Berkshire Hathaway published its annual letter this morning, and it includes details on its AAPL holdings.\",\"content\":\"Warren Buffetts Berkshire Hathaway Inc. published its highly-anticipated annual letter this morning, offer details on the conglomerates investments in 2020. In this years letter, Buffett touted that Apple ranks as Berkshires biggest common stock ... [2562 chars]\",\"url\":\"https:  9to5mac.com 2021 02 27 warren-buffett-aapl-stake-value \",\"image\":\"https:  i2.wp.com 9to5mac.com wp-content uploads sites 6 2018 08 Warren-Buffett.jpg?resize=1200%2C628&quality=82&strip=all&ssl=1\",\"publishedAt\":\"2021-02-27T19:19:39Z\",\"source\":{\"name\":\"9to5Mac\",\"url\":\"https:  9to5mac.com\"}},{\"title\":\"6 Key Questions Apple Answered At The Annual Shareholder Event\",\"description\":\"Shares of Apple Inc (NASDAQ:AAPL) has taken a beating in the recent market sell-off, spearheaded by tech stocks. At its annual shareholder event T...\",\"content\":\"Shares of Apple Inc (NASDAQ:AAPL) has taken a beating in the recent market sell-off, spearheaded by tech stocks. At its annual shareholder event Tuesday, the company assuaged concerns by answering a slew of questions in the Q&A session.Here are the ... [2278 chars]\",\"url\":\"https:  markets.businessinsider.com news stocks 6-key-questions-apple-answered-at-the-annual-shareholder-event-1030120836\",\"image\":\"https:  cdn.benzinga.com files imagecache 1024x768xUP images story 2012 appletv_timcook_2_2_12.png\",\"publishedAt\":\"2021-02-25T07:07:56Z\",\"source\":{\"name\":\"Business Insider\",\"url\":\"https:  markets.businessinsider.com\"}}]}";
            string startDate = "2021-02-25";
            string endDate = "2021-02-28";
            _mockMessageHandler = new MockMessageHandler(mockResponse, HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _gNewsRepository = new GNewsRepository(_httpClient);
            var result = await _gNewsRepository.GetGNewsAsync(validSymbol, startDate, endDate);
            result.Should().NotBeEmpty();
        }

        [Test]
        public async Task GetGNewsAsync_ValidSymbolNoDates_ReturnsIEnumerableOfCompanyNewsArticles()
        {
            string mockResponse = "{\"totalArticles\":2,\"articles\":[{\"title\":\"Warren Buffett praises AAPL stock in annual letter\",\"description\":\"Warren Buffetts Berkshire Hathaway published its annual letter this morning, and it includes details on its AAPL holdings.\",\"content\":\"Warren Buffetts Berkshire Hathaway Inc. published its highly-anticipated annual letter this morning, offer details on the conglomerates investments in 2020. In this years letter, Buffett touted that Apple ranks as Berkshires biggest common stock ... [2562 chars]\",\"url\":\"https:  9to5mac.com 2021 02 27 warren-buffett-aapl-stake-value \",\"image\":\"https:  i2.wp.com 9to5mac.com wp-content uploads sites 6 2018 08 Warren-Buffett.jpg?resize=1200%2C628&quality=82&strip=all&ssl=1\",\"publishedAt\":\"2021-02-27T19:19:39Z\",\"source\":{\"name\":\"9to5Mac\",\"url\":\"https:  9to5mac.com\"}},{\"title\":\"6 Key Questions Apple Answered At The Annual Shareholder Event\",\"description\":\"Shares of Apple Inc (NASDAQ:AAPL) has taken a beating in the recent market sell-off, spearheaded by tech stocks. At its annual shareholder event T...\",\"content\":\"Shares of Apple Inc (NASDAQ:AAPL) has taken a beating in the recent market sell-off, spearheaded by tech stocks. At its annual shareholder event Tuesday, the company assuaged concerns by answering a slew of questions in the Q&A session.Here are the ... [2278 chars]\",\"url\":\"https:  markets.businessinsider.com news stocks 6-key-questions-apple-answered-at-the-annual-shareholder-event-1030120836\",\"image\":\"https:  cdn.benzinga.com files imagecache 1024x768xUP images story 2012 appletv_timcook_2_2_12.png\",\"publishedAt\":\"2021-02-25T07:07:56Z\",\"source\":{\"name\":\"Business Insider\",\"url\":\"https:  markets.businessinsider.com\"}}]}";
            _mockMessageHandler = new MockMessageHandler(mockResponse, HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _gNewsRepository = new GNewsRepository(_httpClient);
            var result = await _gNewsRepository.GetGNewsAsync("AAPL");
            result.Should().NotBeEmpty();
        }

        [Test]
        public async Task GetNewsAsync_ValidSymbolWithOutStartDate_ReturnsIEnumerableOfCompanyNewsArticles()
        {
            string mockResponse = "{\"totalArticles\":2,\"articles\":[{\"title\":\"Warren Buffett praises AAPL stock in annual letter\",\"description\":\"Warren Buffetts Berkshire Hathaway published its annual letter this morning, and it includes details on its AAPL holdings.\",\"content\":\"Warren Buffetts Berkshire Hathaway Inc. published its highly-anticipated annual letter this morning, offer details on the conglomerates investments in 2020. In this years letter, Buffett touted that Apple ranks as Berkshires biggest common stock ... [2562 chars]\",\"url\":\"https:  9to5mac.com 2021 02 27 warren-buffett-aapl-stake-value \",\"image\":\"https:  i2.wp.com 9to5mac.com wp-content uploads sites 6 2018 08 Warren-Buffett.jpg?resize=1200%2C628&quality=82&strip=all&ssl=1\",\"publishedAt\":\"2021-02-27T19:19:39Z\",\"source\":{\"name\":\"9to5Mac\",\"url\":\"https:  9to5mac.com\"}},{\"title\":\"6 Key Questions Apple Answered At The Annual Shareholder Event\",\"description\":\"Shares of Apple Inc (NASDAQ:AAPL) has taken a beating in the recent market sell-off, spearheaded by tech stocks. At its annual shareholder event T...\",\"content\":\"Shares of Apple Inc (NASDAQ:AAPL) has taken a beating in the recent market sell-off, spearheaded by tech stocks. At its annual shareholder event Tuesday, the company assuaged concerns by answering a slew of questions in the Q&A session.Here are the ... [2278 chars]\",\"url\":\"https:  markets.businessinsider.com news stocks 6-key-questions-apple-answered-at-the-annual-shareholder-event-1030120836\",\"image\":\"https:  cdn.benzinga.com files imagecache 1024x768xUP images story 2012 appletv_timcook_2_2_12.png\",\"publishedAt\":\"2021-02-25T07:07:56Z\",\"source\":{\"name\":\"Business Insider\",\"url\":\"https:  markets.businessinsider.com\"}}]}";
            _mockMessageHandler = new MockMessageHandler(mockResponse, HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _gNewsRepository = new GNewsRepository(_httpClient);
            var result = await _gNewsRepository.GetGNewsAsync("AAPL", "", "2021-01-02");
            result.Should().NotBeEmpty();
        }

        [Test]
        public async Task GetNewsAsync_ValidSymbolWithOutEndDate_ReturnsIEnumerableOfCompanyNewsArticles()
        {
            string mockResponse = "{\"totalArticles\":2,\"articles\":[{\"title\":\"Warren Buffett praises AAPL stock in annual letter\",\"description\":\"Warren Buffetts Berkshire Hathaway published its annual letter this morning, and it includes details on its AAPL holdings.\",\"content\":\"Warren Buffetts Berkshire Hathaway Inc. published its highly-anticipated annual letter this morning, offer details on the conglomerates investments in 2020. In this years letter, Buffett touted that Apple ranks as Berkshires biggest common stock ... [2562 chars]\",\"url\":\"https:  9to5mac.com 2021 02 27 warren-buffett-aapl-stake-value \",\"image\":\"https:  i2.wp.com 9to5mac.com wp-content uploads sites 6 2018 08 Warren-Buffett.jpg?resize=1200%2C628&quality=82&strip=all&ssl=1\",\"publishedAt\":\"2021-02-27T19:19:39Z\",\"source\":{\"name\":\"9to5Mac\",\"url\":\"https:  9to5mac.com\"}},{\"title\":\"6 Key Questions Apple Answered At The Annual Shareholder Event\",\"description\":\"Shares of Apple Inc (NASDAQ:AAPL) has taken a beating in the recent market sell-off, spearheaded by tech stocks. At its annual shareholder event T...\",\"content\":\"Shares of Apple Inc (NASDAQ:AAPL) has taken a beating in the recent market sell-off, spearheaded by tech stocks. At its annual shareholder event Tuesday, the company assuaged concerns by answering a slew of questions in the Q&A session.Here are the ... [2278 chars]\",\"url\":\"https:  markets.businessinsider.com news stocks 6-key-questions-apple-answered-at-the-annual-shareholder-event-1030120836\",\"image\":\"https:  cdn.benzinga.com files imagecache 1024x768xUP images story 2012 appletv_timcook_2_2_12.png\",\"publishedAt\":\"2021-02-25T07:07:56Z\",\"source\":{\"name\":\"Business Insider\",\"url\":\"https:  markets.businessinsider.com\"}}]}";
            _mockMessageHandler = new MockMessageHandler(mockResponse, HttpStatusCode.OK);
            _httpClient = new HttpClient(_mockMessageHandler);
            _gNewsRepository = new GNewsRepository(_httpClient);
            var result = await _gNewsRepository.GetGNewsAsync("AAPL", "2021-01-02", "");
            result.Should().NotBeEmpty();
        }
    }
}
