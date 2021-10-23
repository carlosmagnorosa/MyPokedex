using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPokedex.Api.Model
{
    public class AddFavouritePokemonModel
    {        
        public int UserId { get; set; }
        public string PokemonName { get; set; }
    }
}
