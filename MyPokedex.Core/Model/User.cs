using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPokedex.Core.Model
{
    public record FavouritePokemon
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PokemonName { get; set; }
    }
}
