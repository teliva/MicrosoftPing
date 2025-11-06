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

        public async Task<bool> Ping(string stackTrace)
        {
            using var httpClient = new HttpClient();

            string jsonPayload = "{\r\n    \"type\": \"message\",\r\n    \"attachments\": [\r\n        {\r\n            \"contentType\": \"application/vnd.microsoft.card.adaptive\",\r\n            \"contentUrl\": null,\r\n            \"content\": {\r\n                \"$schema\": \"http://adaptivecards.io/schemas/adaptive-card.json\",\r\n                \"type\": \"AdaptiveCard\",\r\n                \"version\": \"1.2\",\r\n                \"body\": [\r\n                    {\r\n                        \"type\": \"TextBlock\",\r\n                        \"text\": \"For Samples and Templates, see [https://adaptivecards.io/samples](https://adaptivecards.io/samples)\"\r\n                    }\r\n                ]\r\n            }\r\n        }\r\n    ]\r\n}";

            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(_webHookURL, content);

            return response.IsSuccessStatusCode;
        }
    }
}