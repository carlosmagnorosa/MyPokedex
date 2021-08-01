using MyPokedex.Core.Exceptions;
using MyPokedex.Core.Model;
using MyPokedex.Core.PokeApi;
using MyPokedex.Infrastructure.PokeApi.Model;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MyPokedex.Infrastructure.PokeApi
{
    public class PokeApiService : IPokeApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IPokeApiSettings _settings;

        public PokeApiService(IHttpClientFactory httpClientFactory, IPokeApiSettings settings)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<BasicPokemon> GetPokemonInfo(string name)
        {
            var pokemonSpecies = await  GetPokemonSpecies(name);

            return new BasicPokemon() 
            {
                Name = pokemonSpecies.Name,
                IsLegendary = pokemonSpecies.IsLegendary,
                Habitat = pokemonSpecies.Habitat?.Name,
                Description = pokemonSpecies.FlavorTextEntries
                            .FirstOrDefault(p=>p.Language.Name.Equals(_settings.FlavorTextLanguage, StringComparison.InvariantCultureIgnoreCase)).FlavorText
             };
        }

        private async Task<PokemonSpecies> GetPokemonSpecies(string name)
        {
            using var httpClient = _httpClientFactory.CreateClient();
            var endPoint = $"{_settings.Endpoint}/{name}";

            using HttpResponseMessage httpResponse = await httpClient.GetAsync(endPoint);
            switch (httpResponse.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return await httpResponse.Content.ReadFromJsonAsync<PokemonSpecies>();
                case System.Net.HttpStatusCode.NotFound:
                    throw new PokemonNotFoundException();
                default:
                    throw new PokeApiException();
            }
        }
    }
}
