using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace StockWebAPI.Unit.Tests.TestHelpers
{
    public class MockMessageHandler : HttpMessageHandler
    {
        private readonly string mockResponse;
        private readonly HttpStatusCode mockStatusCode;

        public MockMessageHandler(string response, HttpStatusCode statusCode)
        {
            this.mockResponse = response;
            this.mockStatusCode = statusCode;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage
            {
                StatusCode = mockStatusCode,
                Content = new StringContent(mockResponse)
            });
        }
    }
}
