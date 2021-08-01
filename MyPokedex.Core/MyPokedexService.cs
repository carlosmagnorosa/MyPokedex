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

        public MyPokedexService(IPokeApiService pokeApiService)
        {
            _pokeApiService = pokeApiService ?? throw new ArgumentNullException(nameof(pokeApiService));
        }

        public async Task<BasicPokemon> GetPokedexBasicInfo(string name)
        {
            return await _pokeApiService.GetPokemonInfo(name.ToLowerInvariant());
        }
    }
}
