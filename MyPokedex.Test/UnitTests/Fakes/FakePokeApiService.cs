using MyPokedex.Core.Model;
using MyPokedex.Core.PokeApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPokedex.Test.UnitTests.Fakes
{
    public class FakePokeApiService : IPokeApiService
    {
        public Task<BasicPokemon> GetPokemonInfo(string name)
        {
            return Task.FromResult(new BasicPokemon()
            {
                Description = "Lorem Ipsum",
                Habitat = "cave",
                IsLegendary = false,
                Name = name
            });
        }
    }
}
