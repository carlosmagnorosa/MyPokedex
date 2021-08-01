using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPokedex.Core;
using MyPokedex.Core.Exceptions;
using MyPokedex.Core.PokeApi;
using MyPokedex.Infrastructure.PokeApi;
using MyPokedex.Infrastructure.PokeApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPokedex.Api.Controllers
{
    [Route("Pokemon")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IMyPokedexService _myPokedexService;

        public PokemonController(IMyPokedexService myPokedexService)
        {
            _myPokedexService = myPokedexService;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            try
            {
                return Ok(await _myPokedexService.GetPokedexBasicInfo(name));
            }
            catch (PokemonNotFoundException)
            {
                return NotFound();
            }
            catch (PokeApiException)
            {
                return StatusCode(503);
            }
        }
    }
}
