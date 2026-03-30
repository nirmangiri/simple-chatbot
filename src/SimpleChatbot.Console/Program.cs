using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatbot.Console
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            string apiKey = "YOUR_OPENAI_API_KEY";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            Console.WriteLine("Welcome to the Simple Chatbot! Type 'exit' to quit.");
            string userInput;

            while ((userInput = Console.ReadLine()) != "exit")
            {
                string response = await GetChatbotResponse(userInput);
                Console.WriteLine($"Chatbot: {response}");
            }
        }

        private static async Task<string> GetChatbotResponse(string userInput)
        {
            var jsonContent = new StringContent(
                $"{{\"model\": \"text-davinci-003\", \"prompt\": \"{userInput}\", \"max_tokens\": 150}}",
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("https://api.openai.com/v1/completions", jsonContent);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return jsonResponse;
        }
    }
}