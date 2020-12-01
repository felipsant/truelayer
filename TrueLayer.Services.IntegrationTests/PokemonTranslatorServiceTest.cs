using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrueLayer.Entities;

namespace TrueLayer.Services.IntegrationTests
{
    [TestClass]
    public class PokemonTranslatorServiceTest
    {
        private PokemonTranslatorService pokemonTranslatorService;
        public PokemonTranslatorServiceTest()
        {
            pokemonTranslatorService = new PokemonTranslatorService();
        }
        
        [TestMethod]
        public async Task GetPokemonTranslation_Should_ReturnPokemon()
        {
            //Arrange
            string name = "pikachu";

            //Act
            var result = await pokemonTranslatorService.
                GetPokemonTranslation(name);

            //Assert
            Assert.IsTrue(!string.IsNullOrEmpty(result.description));
            Assert.IsTrue(result.name == name);
        }
    }
}
