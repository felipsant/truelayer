using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace TrueLayer.Services
{
    /// <summary>
    /// This Service class is responsible for making calls to the Translator
    /// </summary>
    public class ShakespeareService
    {
        private HttpClient httpClient;
        public ShakespeareService()
        {
            this.httpClient = new HttpClient()
            {
                BaseAddress = new System.Uri(
                    "https://api.funtranslations.com/")
            };
        }

        /// <summary>
        /// Post an text to the shakespeare api and receives the converted
        /// desccription. Currently not using API Keys since it is the free
        /// version.
        /// </summary>
        /// <param name="toBeTranslated"></param>
        /// <returns>Converted Shakespeare description or null</returns>
        public async Task<string> TranslateText(string toBeTranslated)
        {
            string result = null;
            string text = HttpUtility.UrlEncode(toBeTranslated);
            var response = await httpClient.GetAsync($"translate/" +
                $"shakespeare.json?text=" + $"{text}");
            if (response.IsSuccessStatusCode) {

                dynamic contentResult = JsonConvert.DeserializeObject(
                    await response.Content.ReadAsStringAsync());
                result = contentResult["contents"]["translated"];
            }
            return result;
        }
    }
}
