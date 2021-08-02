using Moq;
using Moq.Protected;
using MyPokedex.Test.ApiResponses;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MyPokedex.Test.Helpers
{
    internal static class HttpClientFactoryMoq
    {
        public static Mock<IHttpClientFactory> GetHttpClientFactoryMoq(HttpStatusCode statusCode, string jsonResponse)
        {
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                 .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = new StringContent(jsonResponse)
                });

            var httpClient = new HttpClient(mockMessageHandler.Object);

            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            return mockFactory;
        }
    }
}