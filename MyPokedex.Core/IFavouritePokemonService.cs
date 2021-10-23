using MyPokedex.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPokedex.Core
{
    public interface IFavouritePokemonService
    {
        Task<List<string>> GetFavouritePokemons(int uid);
        Task AddFavouritePokemon(int uid, string name);
    }
}
