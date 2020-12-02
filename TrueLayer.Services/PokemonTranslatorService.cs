using System;
using System.Threading.Tasks;
using TrueLayer.Entities;
using TrueLayer.Models;
using TrueLayer.Repositories;
namespace TrueLayer.Services
{
    /// <summary>
    /// This Service class searchs for a pokemon and returns it's shakespeare 
    /// description Translation. It consumes both PokeApiNet and Shakespeare 
    /// translator. This creates 2 points of possible communication failures.
    /// To avoid possible problems it will use a local DB to store/get searchs.
    /// </summary>
    public class PokemonTranslatorService
    {
        public PokeAPIService pokeService;
        private ShakespeareService shakeService;
        public GenericRepository<PokemonEntity, string> pokeRepository;
        private PokemonDBContext pokemonDBContext;
        public PokemonTranslatorService()
        {
            pokeService = new PokeAPIService();
            shakeService = new ShakespeareService();
            pokemonDBContext = new PokemonDBContext();
            pokeRepository = new GenericRepository<PokemonEntity, string>
                (pokemonDBContext);
        }
        public PokemonTranslatorService(PokeAPIService _pokeService,
            ShakespeareService _shakeService,
            PokemonDBContext _pokemonDBContext,
            GenericRepository<PokemonEntity, string> _pokeRepository
            )
        {
            pokeService = _pokeService;
            shakeService = _shakeService;
            pokemonDBContext = _pokemonDBContext;
            pokeRepository = _pokeRepository;
        }

        /// <summary>
        /// Consumes both PokeApiNet and Shakespeare translator API's in order 
        /// to return the desired pokemon shakespeared description
        /// Uses DB version in case of previous search.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Pokemon model with both name and shakespeare translation
        /// </returns>
        public async Task<Pokemon> GetPokemonTranslation(string name)
        {
            PokemonEntity pokemon = null;
            try
            {
                pokemon = pokeRepository.GetByID(name);
                if (pokemon == null)
                {
                    string description = await pokeService.
                        GetPokemonDescription(name);
                    string shakeDescription = await shakeService.
                        TranslateText(description);
                    pokemon = new PokemonEntity()
                    {
                        name = name,
                        description = description,
                        shakespeareDescription = shakeDescription
                    };
                    pokeRepository.Insert(pokemon);
                    await pokemonDBContext.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                //Fail the search in case of exception
                return null;
            }

            Pokemon result = new Pokemon()
            {
                description = pokemon.shakespeareDescription,
                name = name
            };
            return result;
        }
    }
}
