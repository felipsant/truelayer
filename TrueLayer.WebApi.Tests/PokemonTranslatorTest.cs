
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace TrueLayer.WebApi.IntegrationTests
{
    [TestClass]
    public class PokemonTranslatorTest
    {
        private HttpClient _client;
        public PokemonTranslatorTest()
        {
            _client = new HttpClient(){
                BaseAddress = new System.Uri("http://localhost:5000/")
            };
            this._client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [TestMethod]
        public async Task Get_Should_ReturnListOfPokemonNames()
        {
            //Arrange
            //Act
            var response = await _client.GetAsync("pokemon/");
            
            //Assert
            Assert.IsTrue(response.IsSuccessStatusCode);
            var content = await response.Content.ReadAsStringAsync();
            
            dynamic result = JsonConvert.DeserializeObject(content);
            Assert.IsTrue(result.Count > 0);
        }
    }
}
