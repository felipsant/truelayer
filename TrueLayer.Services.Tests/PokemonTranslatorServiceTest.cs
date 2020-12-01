using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TrueLayer.Entities;
using TrueLayer.Repositories;

namespace TrueLayer.Services.Tests
{
    [TestClass]
    public class PokemonTranslatorServiceTest
    {
        private Mock<PokemonDBContext> mockPokemonDBContext;
        private Mock<GenericRepository<PokemonEntity, 
            string>> mockPokemonRepository;
        private Mock<ShakespeareService> mockShakeService;
        private Mock<PokeAPIService> mockPokeApiService;
        private PokemonTranslatorService ptService;
        [TestInitialize]
        public virtual void Setup()
        {
            mockPokemonDBContext = new Mock<PokemonDBContext>() { };
            mockPokemonRepository = new Mock<GenericRepository<PokemonEntity,
                string>>(mockPokemonDBContext.Object);
            mockShakeService = new Mock<ShakespeareService>();
            mockPokeApiService = new Mock<PokeAPIService>();
            ptService = new PokemonTranslatorService(
                mockPokeApiService.Object,
                mockShakeService.Object,
                mockPokemonDBContext.Object,
                mockPokemonRepository.Object);
        }

        [TestMethod]
        public async Task GetPokemonTranslation_Should_ReturnPokemon()
        {
            //Arrange
            string name = "pikachu";
            mockPokeApiService.Setup(c => c.GetPokemonDescription(
                name)).Returns(Task.FromResult("description"));

            mockShakeService.Setup(c => c.TranslateText(
                It.IsAny<string>())).Returns(
                Task.FromResult("shake description"));

            //Act
            var result = await ptService.GetPokemonTranslation(name);

            //Assert
            Assert.IsTrue(!string.IsNullOrEmpty(result.description));
            Assert.IsTrue(!string.IsNullOrEmpty(result.name));
        }
    }
}
