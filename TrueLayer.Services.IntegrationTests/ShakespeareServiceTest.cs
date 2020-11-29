using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TrueLayer.Services.IntegrationTests
{
    [TestClass]
    public class ShakespeareServiceTest
    {
        private ShakespeareService shakeService;
        public ShakespeareServiceTest()
        {
            shakeService = new ShakespeareService();
        }

        [TestMethod]
        public async Task TranslateText_Should_ConvertText()
        {
            //Arrange
            string description = "It deliberately makes itself heavy so it can with­ stand the recoil of the water jets it fires.";

            //Act
            var result = await shakeService.TranslateText(description);

            //Assert
            Assert.IsTrue(!string.IsNullOrEmpty(result));
        }

    }
}
