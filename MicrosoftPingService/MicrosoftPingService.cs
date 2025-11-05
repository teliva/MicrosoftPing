using System.Runtime.CompilerServices;
using System.Text;

namespace Services
{
    public class MicrosoftPingService
    {
        private string _webHookURL;

        public MicrosoftPingService(string webHookURL)
        {
            _webHookURL = webHookURL;
        }

        public async void Ping(string message)
        {
            using var httpClient = new HttpClient();

            // Teams expects valid JSON. For simple text messages, use this:
            string jsonPayload = "{ \"text\": \"Hello from C#!\" }";

            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(_webHookURL, content);

            if (response.IsSuccessStatusCode)
                Console.WriteLine("Message sent successfully!");
            else
                Console.WriteLine($"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
        }
    }
}