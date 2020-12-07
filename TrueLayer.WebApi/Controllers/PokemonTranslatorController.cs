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

        /// <summary>
        /// Initialization for this method expects an PokemonTranslatorService
        /// and the ILogger.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="ptService"></param>
        public PokemonTranslatorController(
            ILogger<PokemonTranslatorController> logger,
            PokemonTranslatorService ptService
        )
        {
            _logger = logger;
            _ptService = ptService;
        }

        /// <summary>
        /// Get the List of Pokemon names that can be used 
        /// </summary>
        /// <returns>List with the valid Pokemon names</returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            try
            {
                return _ptService.pokeService.lPokemon.Select(c => c.Name)
                    .ToList();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Pokemon shakespeare description 
        /// </summary>
        /// <returns>String shakespeare description </returns>
        /// <param name="pokemon" example="pikachu">The name of the pokemon</param>
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
