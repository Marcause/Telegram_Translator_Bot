using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; 
using Newtonsoft.Json.Linq;

namespace TranslatorBot
{
    internal class Translator
    {
        private static readonly string location = "westeurope";
        private static readonly string key = new Claves().translatorKey;
        private static readonly string endpoint = new Claves().translatorEndopoint;

        public string textToTranslate;
        public string route;

        public async Task<string> translate(string textToTranslate, string languageFrom = "es", string languageTo = "en")
        {
            try
            {
                this.textToTranslate = textToTranslate;

                // Input and output languages are defined as parameters.
                this.route = $"/translate?api-version=3.0&from={languageFrom}&to={languageTo}";
                Console.WriteLine(this.route);


                object[] body = new object[] { new { Text = textToTranslate } };
                var requestBody = JsonConvert.SerializeObject(body);

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage())
                {
                    // Build the request.
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri(endpoint + route);
                    request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    request.Headers.Add("Ocp-Apim-Subscription-Key", key);
                    request.Headers.Add("Ocp-Apim-Subscription-Region", location);

                    // Send the request and get response.
                    HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);

                    // Read response as a string.
                    var translationObject = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(translationObject);

                    var JsonTranslation = JArray.Parse(translationObject);
                    Console.WriteLine(JsonTranslation);

                    //Return of the translated text
                    return (string)JsonTranslation[0]["translations"][0]["text"];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "Ha ocurrido un error";
            }
        }
    }
}
