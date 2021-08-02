using Microsoft.Extensions.Options;
using MyPokedex.Core.Config;
using MyPokedex.Core.External.FunTranslations;
using MyPokedex.Core.Model;
using MyPokedex.Core.PokeApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPokedex.Core
{
    public class MyPokedexService : IMyPokedexService
    {
        private readonly IPokeApiService _pokeApiService;
        private readonly IFunTranslationService _funTranslationService;
        private readonly TranslatedPokemonOptions _translatedPokemonOptions;

        public MyPokedexService(IPokeApiService pokeApiService, IFunTranslationService funTranslationService, IOptions<TranslatedPokemonOptions> translatedPokemonOptions)
        {
            _pokeApiService = pokeApiService ?? throw new ArgumentNullException(nameof(pokeApiService));
            _funTranslationService = funTranslationService ?? throw new ArgumentNullException(nameof(funTranslationService));
            _translatedPokemonOptions = translatedPokemonOptions?.Value ?? throw new ArgumentNullException(nameof(translatedPokemonOptions));
        }

        public async Task<BasicPokemon> GetPokedexBasicInfo(string name)
        {
            return await _pokeApiService.GetPokemonInfo(name.ToLowerInvariant());
        }

        public async Task<BasicPokemon> GetPokedexTranslatedInfo(string name)
        {
            return await GetPokedexTranslatedInfo(await GetPokedexBasicInfo(name));
        }

        public async Task<BasicPokemon> GetPokedexTranslatedInfo(BasicPokemon pokemon)
        {
            string newDescription = string.Empty;
            if (pokemon.IsLegendary || pokemon.Habitat.Equals(_translatedPokemonOptions.HabitatNameRule, StringComparison.InvariantCultureIgnoreCase))
            {
                newDescription = await _funTranslationService.TranslateEnglishToYoda(pokemon.Description);
            }
            else
            {
                newDescription = await _funTranslationService.TranslateEnglishToShakespeare(pokemon.Description);
            }

            return new BasicPokemon()
            {
                Description = newDescription,
                Habitat = pokemon.Habitat,
                IsLegendary = pokemon.IsLegendary,
                Name = pokemon.Name
            };
        }
    }
}
