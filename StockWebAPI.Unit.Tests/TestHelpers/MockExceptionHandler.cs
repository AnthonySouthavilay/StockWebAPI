using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace StockWebAPI.Unit.Tests.TestHelpers
{
    public class MockExceptionHandler : HttpMessageHandler
    {
        private Exception mockException;
        public MockExceptionHandler(Exception exception)
        {
            this.mockException = exception;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            throw mockException;
        }
    }
}
