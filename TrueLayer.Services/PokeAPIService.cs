using PokeApiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrueLayer.Services
{
    /// <summary>
    /// This Service class is reponsible for Consuming PokeApiNet Nuget Package.
    /// </summary>
    public class PokeAPIService
    {
        /// <summary>
        /// This Client has to be treated carefully, it has cache enabled by 
        /// default.
        /// </summary>
        private PokeApiClient pokeClient;
        public List<NamedApiResource<Pokemon>> lPokemon = null;
        public PokeAPIService()
        {
            pokeClient = new PokeApiClient();
            SetPokemonList().Wait();
        }

        public PokeAPIService(PokeApiClient nPokeClient)
        {
            pokeClient = nPokeClient;
            SetPokemonList().Wait();
        }

        /// <summary>
        /// Sets the list of valid pokemon names.
        /// </summary>
        private async Task SetPokemonList()
        {
            var clientResult = await pokeClient.GetNamedResourcePageAsync
                <Pokemon>(-1,0, default);
            lPokemon = clientResult.Results;
        }

        /// <summary>
        /// Checks for the Name of the Pokemon in the list of pokemons
        /// Searchs for the full data of a Pokemon
        /// </summary>
        /// <param name="name"></param>
        /// <returns>PokeApi.Pokemon or Null</returns>
        public async Task<Pokemon> GetPokemon(string name)
        {
            if(lPokemon is null)
            {
                await SetPokemonList();
            }
            var pokemon = lPokemon.FirstOrDefault(c => c.Name == name);
            if (pokemon is null)
            {
                return null;
            }

            return await pokeClient.GetResourceAsync<Pokemon>(
                pokemon);
        }

        /// <summary>
        /// Get's the english description of a pokemon by it's name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual async Task<string> GetPokemonDescription(string name)
        {
            // Find pokemon Id by it's name
            var pokemon = await GetPokemon(name);

            //Get the Characteristics 
            PokemonSpecies specie = await pokeClient.GetResourceAsync
                <PokemonSpecies>(pokemon.Species);

            //Filter for English Description
            //TODO: Ask with version to use.
            var description = specie.FlavorTextEntries.FirstOrDefault(
                c => c.Language.Name == "en");
            
            return description?.FlavorText;
        }
    }
}
