using MyPokedex.Core;
using MyPokedex.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPokedex.Infrastructure
{
    public class FavouritePokemonService : IFavouritePokemonService
    {
        private readonly PokeRepo pokeRepo;

        public FavouritePokemonService(PokeRepo pokeRepo)
        {
            this.pokeRepo = pokeRepo ?? throw new ArgumentNullException(nameof(pokeRepo));
        }

        public async Task AddFavouritePokemon(int uid, string name)
        {
            await pokeRepo.Pokemons.AddAsync(new Core.Model.FavouritePokemon()
            {
                PokemonName = name,
                UserId = uid
            });

            await pokeRepo.SaveChangesAsync();
        }

        public async Task<List<string>> GetFavouritePokemons(int uid)
        {
            return await Task.FromResult(pokeRepo.Pokemons.Where(p => p.UserId == uid).Select(p => p.PokemonName).ToList());
        }
    }
}
