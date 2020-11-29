using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using TrueLayer.Services;

namespace TrueLayer.Services.IntegrationTests
{
    [TestClass]
    public class PokeAPIServiceTest
    {
        private PokeAPIService pokeApi;
        public PokeAPIServiceTest()
        {
            pokeApi = new PokeAPIService();
        }
        [TestMethod]
        public async Task SetPokemonList_Should_FillPokemonList()
        {
            //Arrange
            //Act
            await pokeApi.SetPokemonList();

            //Assert
            Assert.IsTrue(pokeApi.lPokemon.Count >= 0);
        }

        [TestMethod]
        public async Task GetPokemon_Should_ReturnPokemonData()
        {
            //Arrange
            await pokeApi.SetPokemonList();
            int randomPokemonNumber = new Random().Next(1, pokeApi.
                lPokemon.Count);
            var randomPokemon = pokeApi.lPokemon[randomPokemonNumber];

            //Act
            var pokemon = await pokeApi.GetPokemon(randomPokemon.Name);

            //Assert
            Assert.IsTrue(pokemon != null);
        }

        [TestMethod]
        public async Task GetPokemonDescription_Should_ReturnDescription()
        {
            //Arrange
            await pokeApi.SetPokemonList();
            int randomPokemonNumber = new Random().Next(0, pokeApi.
                lPokemon.Count-1);
            var randomPokemon = pokeApi.lPokemon[randomPokemonNumber];

            //Act
            string description = await pokeApi.GetPokemonDescription(
                randomPokemon.Name);

            //Assert
            Assert.IsTrue(!string.IsNullOrEmpty(description));
        }
    }
}
