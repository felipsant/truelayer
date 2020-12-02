using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PokeApiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueLayer.Services;

namespace TrueLayer.WebApi.Controllers
{
    [ApiController]
    [Route("pokemon/")]
    public class PokemonTranslatorController : ControllerBase
    {
        private readonly ILogger<PokemonTranslatorController> _logger;
        private PokemonTranslatorService _ptService;

        public PokemonTranslatorController(
            ILogger<PokemonTranslatorController> logger,
            PokemonTranslatorService ptService
        )
        {
            _logger = logger;
            _ptService = ptService;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            try
            {
                return _ptService.pokeService.lPokemon.Select(c => c.Name).ToList();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("{pokemon}")]
        public async Task<string> Get(string pokemon)
        {
            try
            {
                var result = await _ptService.GetPokemonTranslation(pokemon);
                return result.description;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
