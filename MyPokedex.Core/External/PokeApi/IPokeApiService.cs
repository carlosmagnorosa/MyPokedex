
using MyPokedex.Core.Model;
using System.Threading.Tasks;

namespace MyPokedex.Core.PokeApi
{
    public interface IPokeApiService
    {
        Task<BasicPokemon> GetPokemonInfo(string name);
    }
}