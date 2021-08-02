using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MyPokedex.Api;
using MyPokedex.Core.PokeApi;
using MyPokedex.Infrastructure.PokeApi;
using MyPokedex.Test.ApiResponses;
using MyPokedex.Test.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyPokedex.Test.IntegrationTests
{
    public class TestGetBasicPokemonInfo : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public TestGetBasicPokemonInfo(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        private WebApplicationFactory<Startup> CreateWebApp(Mock<IHttpClientFactory> mockHttpClientFactory) {
            return _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, config) =>
                {
                    config
                       .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.test.json")
                        .AddEnvironmentVariables();
                });
                builder.ConfigureTestServices(services =>
                {
                    //override IHttpClientFactory
                    services.AddTransient<IPokeApiService>((sp) => new PokeApiService(mockHttpClientFactory.Object, sp.GetService<IPokeApiSettings>(),
                        sp.GetService<IMemoryCache>()));
                });
            });
        }

        [Fact(Skip="Do not autorun, as it uses the real external dependency")]
        public async Task GetBasicInfoOfKnownPokemon_Return200OkAndExpectedContent()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/pokemon/mewtwo");
            var jsonString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Contains("{\"name\":\"mewtwo\"", jsonString);
        }

        [Fact]
        public async Task GetBasicInfoOfKnownPokemon_Return200Ok()
        {
            var mockHttpClientFactory = HttpClientFactoryMoq.GetHttpClientFactoryMoq(System.Net.HttpStatusCode.OK, PokemonSpeciesApiResponses.DittoJsonResponse);

            // Arrange
            var client = CreateWebApp(mockHttpClientFactory).CreateClient();

            // Act
            var response = await client.GetAsync("/pokemon/ditto");
            var jsonString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetBasicInfoOfKnownPokemon_ReturnExpectedBody()
        {
            var mockHttpClientFactory = HttpClientFactoryMoq.GetHttpClientFactoryMoq(System.Net.HttpStatusCode.OK, PokemonSpeciesApiResponses.DittoJsonResponse);

            // Arrange
            var client = CreateWebApp(mockHttpClientFactory).CreateClient();

            // Act
            var response = await client.GetAsync("/pokemon/ditto");
            var jsonString = await response.Content.ReadAsStringAsync();

            // Assert

            Assert.Equal("{\"name\":\"ditto\",\"description\":\"It can freely recombine its own cellular structure to\\ntransform into other life-forms.\",\"habitat\":\"urban\",\"isLegendary\":false}",
                jsonString);

        }

        [Fact]
        public async Task GetBasicInfoOfUnknownPokemon_Return404NotFound()
        {
            var mockHttpClientFactory = HttpClientFactoryMoq.GetHttpClientFactoryMoq(System.Net.HttpStatusCode.NotFound, string.Empty);

            // Arrange
            var client = CreateWebApp(mockHttpClientFactory).CreateClient();

            // Act
            var response = await client.GetAsync("/pokemon/foobar");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetBasicInfoOfknownPokemonWithUnknownLanguage_Return503ServiceUnavailable()
        {
            var mockHttpClientFactory = HttpClientFactoryMoq.GetHttpClientFactoryMoq(System.Net.HttpStatusCode.TooManyRequests, PokemonSpeciesApiResponses.DittoJsonResponse);

            // Arrange
            var client = CreateWebApp(mockHttpClientFactory).CreateClient();

            // Act
            var response = await client.GetAsync("/pokemon/foobar");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.ServiceUnavailable, response.StatusCode);
        }
    }
}
