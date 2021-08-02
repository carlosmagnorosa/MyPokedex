using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using MyPokedex.Api;
using MyPokedex.Core.External.FunTranslations;
using MyPokedex.Core.PokeApi;
using MyPokedex.Infrastructure.FunTranslations;
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
    public class TestGetTranslatedPokemonInfo : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public TestGetTranslatedPokemonInfo(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        private WebApplicationFactory<Startup> CreateWebApp(Mock<IHttpClientFactory> pokeApiHttpClientFactory, Mock<IHttpClientFactory> funTranslationServiceHttpClientFactory)
        {
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
                    services.AddTransient<IPokeApiService>((sp) => new PokeApiService(pokeApiHttpClientFactory.Object, sp.GetService<IPokeApiSettings>(),
                        sp.GetService<IMemoryCache>()));
                    services.AddTransient<IFunTranslationService>(
                       (sp) => new FunTranslationService(funTranslationServiceHttpClientFactory.Object, 
                       sp.GetService<IOptions<FunTranslationOptions>>(), 
                       sp.GetService<IMemoryCache>()));
                });
            });
        }

        [Fact]
        public async Task GetTranslatedInfoOfKnownPokemon_Return200Ok()
        {
            var pokeApiServiceHttpClientFactory = HttpClientFactoryMoq.GetHttpClientFactoryMoq(System.Net.HttpStatusCode.OK, PokemonSpeciesApiResponses.DittoJsonResponse);
            var funTranslationServiceHttpClientFactory = HttpClientFactoryMoq.GetHttpClientFactoryMoq(System.Net.HttpStatusCode.OK, FunTranslationsApiResponses.YodaTranslation);

            // Arrange
            var client = CreateWebApp(pokeApiServiceHttpClientFactory, funTranslationServiceHttpClientFactory).CreateClient();

            // Act
            var response = await client.GetAsync("/pokemon/translated/ditto");
            var jsonString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetTranslatedInfoOfKnownPokemon_ReturnExpectedBody()
        {
            var pokeApiServiceHttpClientFactory = HttpClientFactoryMoq.GetHttpClientFactoryMoq(System.Net.HttpStatusCode.OK, PokemonSpeciesApiResponses.DittoJsonResponse);
            var funTranslationServiceHttpClientFactory = HttpClientFactoryMoq.GetHttpClientFactoryMoq(System.Net.HttpStatusCode.OK, FunTranslationsApiResponses.YodaTranslation);

            // Arrange
            var client = CreateWebApp(pokeApiServiceHttpClientFactory, funTranslationServiceHttpClientFactory).CreateClient();

            // Act
            var response = await client.GetAsync("/pokemon/translated/ditto");
            var jsonString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal("{\"name\":\"ditto\",\"description\":\""+ FunTranslationsApiResponses.YodaDescription +"\",\"habitat\":\"urban\",\"isLegendary\":false}",
                jsonString);

        }

        [Fact]
        public async Task GetTranslatedInfoOfUnknownPokemon_Return404NotFound()
        {
            var expectedDescription = FunTranslationsApiResponses.YodaTranslation;
            var pokeApiServiceHttpClientFactory = HttpClientFactoryMoq.GetHttpClientFactoryMoq(System.Net.HttpStatusCode.NotFound, PokemonSpeciesApiResponses.DittoJsonResponse);
            var funTranslationServiceHttpClientFactory = HttpClientFactoryMoq.GetHttpClientFactoryMoq(System.Net.HttpStatusCode.OK, expectedDescription);

            // Arrange
            var client = CreateWebApp(pokeApiServiceHttpClientFactory, funTranslationServiceHttpClientFactory).CreateClient();

            // Act
            var response = await client.GetAsync("/pokemon/translated/ditto");
            var jsonString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetTranslatedInfoWithRateLimitedFunTranslationService_Return503ServiceUnavailable()
        {
            var expectedDescription = FunTranslationsApiResponses.YodaTranslation;
            var pokeApiServiceHttpClientFactory = HttpClientFactoryMoq.GetHttpClientFactoryMoq(System.Net.HttpStatusCode.OK, PokemonSpeciesApiResponses.DittoJsonResponse);
            var funTranslationServiceHttpClientFactory = HttpClientFactoryMoq.GetHttpClientFactoryMoq(System.Net.HttpStatusCode.TooManyRequests, string.Empty);

            // Arrange
            var client = CreateWebApp(pokeApiServiceHttpClientFactory, funTranslationServiceHttpClientFactory).CreateClient();

            // Act
            var response = await client.GetAsync("/pokemon/translated/ditto");
            var jsonString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.ServiceUnavailable, response.StatusCode);
        }
    }
}
