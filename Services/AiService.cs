using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace YourNamespace.Service
{
    public class AiService
    {
        private readonly string? _apiKey;
        private readonly HttpClient _httpClient;

        public AiService(IConfiguration configuration)
        {
            // Read from appsettings.json: Gemini:ApiKey
            _apiKey = configuration["Gemini:ApiKey"];
            if (string.IsNullOrWhiteSpace(_apiKey))
                throw new InvalidOperationException("Gemini API key is not configured!");
            Console.WriteLine("API Key: ", _apiKey);
            _httpClient = new HttpClient();
            // _httpClient.Timeout = TimeSpan.FromSeconds(60); // e.g., 60 seconds

        }

        public async Task<string> AskGeminiAsync(string prompt)
        {
            try
            {
                var requestBody = $@"{{
                    ""contents"": [{{
                        ""role"": ""user"",
                        ""parts"": [{{ ""text"": ""{prompt.Replace("\"", "\\\"")}"" }}]
                    }}]
                }}";

                var request = new HttpRequestMessage(HttpMethod.Post, "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-pro:generateContent?key=" + _apiKey);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                System.IO.File.AppendAllText("gemini_prompts.log", $"{DateTime.Now}: {prompt}\n");

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                // Parse JSON to extract answer text (robust!)
                using var doc = JsonDocument.Parse(content);
                var root = doc.RootElement;
                var text = root
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                return text ?? "[No answer text returned]";
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("gemini_errors.log", $"{DateTime.Now}: {ex.Message}\n{ex.StackTrace}\n");
                return $"[Gemini ERROR]: {ex.Message}\n{ex.StackTrace}";
            }
            // var requestBody = $@"{{
            //     ""contents"": [{{
            //         ""role"": ""user"",
            //         ""parts"": [{{ ""text"": ""{prompt.Replace("\"", "\\\"")}"" }}]
            //     }}]
            // }}";

            // var request = new HttpRequestMessage(HttpMethod.Post, "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-pro:generateContent?key=" + _apiKey);
            // request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            // var response = await _httpClient.SendAsync(request);
            // response.EnsureSuccessStatusCode();
            // var content = await response.Content.ReadAsStringAsync();

            // // Extract just the response text
            // // The Gemini API response JSON looks like:
            // // { "candidates": [ { "content": { "parts": [ { "text": "answer here" } ] } } ] }
            // var marker = @"""text"":""";
            // var idx = content.IndexOf(marker);
            // if (idx > 0)
            // {
            //     var start = idx + marker.Length;
            //     var end = content.IndexOf("\"", start);
            //     if (end > start)
            //         return content.Substring(start, end - start);
            // }
            // return content;
        }
    }
}
