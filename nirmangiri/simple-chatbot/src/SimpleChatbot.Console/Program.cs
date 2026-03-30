using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Program
{
    private static readonly HttpClient client = new HttpClient();
    private const string apiKey = "YOUR_OPENAI_API_KEY"; // Replace with your OpenAI API key

    static async Task Main(string[] args)
    {
        Console.WriteLine("Welcome to the Chatbot! Type 'exit' to end the conversation.");

        while (true)
        {
            Console.Write("You: ");
            string userInput = Console.ReadLine();

            if (userInput.ToLower() == "exit")
            {
                break;
            }

            string response = await GetChatbotResponse(userInput);
            Console.WriteLine($"Chatbot: {response}");
        }
    }

    private static async Task<string> GetChatbotResponse(string input)
    {
        var requestBody = new {
            model = "gpt-3.5-turbo",
            messages = new[] {
                new { role = "user", content = input }
            }
        };

        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

        var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);

        return jsonResponse.choices[0].message.content.ToString();
    }
}