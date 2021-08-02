using Moq;
using MyPokedex.Core.Exceptions;
using MyPokedex.Core.PokeApi;
using MyPokedex.Infrastructure.PokeApi;
using MyPokedex.Test.ApiResponses;
using MyPokedex.Test.Helpers;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MyPokedex.Test.UnitTests
{
    public class TestPokeApiService
    {
        [Fact]
        public async Task PassKnownPokemonName_ReturnsValidPokemonSpecies()
        {
            // Arrange
            var mockPokeApiSettings = new Mock<IPokeApiSettings>();
            mockPokeApiSettings.SetupGet(x => x.Endpoint).Returns("http://localhost");
            mockPokeApiSettings.SetupGet(x => x.FlavorTextLanguage).Returns("en");
            Mock<IHttpClientFactory> httpClientFactory = HttpClientFactoryMoq.GetHttpClientFactoryMoq(
                                                            HttpStatusCode.OK,
                                                            PokemonSpeciesApiResponses.DittoJsonResponse);

            // Act
            PokeApiService pokeApiService = new PokeApiService(httpClientFactory.Object, mockPokeApiSettings.Object);
            var response = await pokeApiService.GetPokemonInfo("ditto");


            // Assert
            Assert.Equal("ditto", response.Name);
            Assert.Equal("urban", response.Habitat);
            Assert.False(response.IsLegendary);
        }

        [Fact]
        public async Task PassUnkownPokemonName_ThrowsPokemonNotFoundException()
        {
            // Arrange
            var mockPokeApiSettings = new Mock<IPokeApiSettings>();
            mockPokeApiSettings.SetupGet(x => x.Endpoint).Returns("http://localhost");
            mockPokeApiSettings.SetupGet(x => x.FlavorTextLanguage).Returns("en");
            Mock<IHttpClientFactory> httpClientFactory = HttpClientFactoryMoq.GetHttpClientFactoryMoq(
                                                            HttpStatusCode.NotFound,
                                                            string.Empty);
            PokeApiService pokeApiService = new PokeApiService(httpClientFactory.Object, mockPokeApiSettings.Object);

            // Act Assert
            await Assert.ThrowsAsync<PokemonNotFoundException>(async () => await pokeApiService.GetPokemonInfo("foobar"));
        }


        [Fact]
        public async Task ReturnsPokemonWithNoValidDescription_ThrowsPokeApiError()
        {
            // Arrange
            var mockPokeApiSettings = new Mock<IPokeApiSettings>();
            mockPokeApiSettings.SetupGet(x => x.Endpoint).Returns("http://localhost");
            mockPokeApiSettings.SetupGet(x => x.FlavorTextLanguage).Returns("UK");
            Mock<IHttpClientFactory> httpClientFactory = HttpClientFactoryMoq.GetHttpClientFactoryMoq(
                                                            HttpStatusCode.OK,
                                                            PokemonSpeciesApiResponses.DittoJsonResponse);

            PokeApiService pokeApiService = new PokeApiService(httpClientFactory.Object, mockPokeApiSettings.Object);

            // Act Assert
            await Assert.ThrowsAsync<PokeApiException>(async () => await pokeApiService.GetPokemonInfo("ditto"));
        }
    }
}
