using System;
using System.Threading.Tasks;
using OpenAI_API;

namespace SimpleChatbot.API.Services
{
    public class ChatService
    {
        private readonly OpenAIAPI _openAIAPI;

        public ChatService(string apiKey)
        {
            _openAIAPI = new OpenAIAPI(apiKey);
        }

        public async Task<string> GetChatResponse(string userInput)
        {
            var chatRequest = new CompletionRequest
            {
                Prompt = userInput,
                MaxTokens = 150,
                Temperature = 0.7,
            };

            var response = await _openAIAPI.Completions.CreateAsync(chatRequest);
            return response.Completions[0].Text.Trim();
        }
    }
}