using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPokedex.Api.Model;
using MyPokedex.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPokedex.Api.Controllers
{
    [Route("FavouritePokemons")]
    [ApiController]
    public class MyPokemonController : ControllerBase
    {
        private readonly IFavouritePokemonService favouritePokemonService;

        public MyPokemonController(IFavouritePokemonService favouritePokemonService)
        {
            this.favouritePokemonService = favouritePokemonService ?? throw new ArgumentNullException(nameof(favouritePokemonService));
        }

        [HttpPost]
        public async Task<IActionResult> AddFavouritePokemon([FromBody] AddFavouritePokemonModel request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            await this.favouritePokemonService.AddFavouritePokemon(request.UserId, request.PokemonName);
            return Ok();
        }

        [HttpGet("{uid}")]
        public async Task<IActionResult> GetFavouritePokemons(int uid)
        {
            return Ok(await this.favouritePokemonService.GetFavouritePokemons(uid));
        }
    }
}
