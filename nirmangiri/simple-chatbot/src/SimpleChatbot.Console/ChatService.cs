using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatbot.Console
{
    public class ChatService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiEndpoint;

        public ChatService(string apiKey)
        {
            _httpClient = new HttpClient();
            _apiKey = apiKey;
            _apiEndpoint = "https://api.openai.com/v1/chat/completions";
        }

        public async Task<string> GetChatbotResponse(string userMessage)
        {
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new []
                {
                    new { role = "user", content = userMessage }
                }
            };

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiEndpoint, content);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return responseData;
        }
    }
}