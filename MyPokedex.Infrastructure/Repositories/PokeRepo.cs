using Microsoft.EntityFrameworkCore;
using MyPokedex.Core.Data;
using MyPokedex.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPokedex.Infrastructure.Repositories
{
    public class PokeRepo :  DbContext
    {
        public PokeRepo(DbContextOptions<PokeRepo> options) : base(options) 
        {

        }

        public DbSet<FavouritePokemon> Pokemons { get; set; }
    }
}
