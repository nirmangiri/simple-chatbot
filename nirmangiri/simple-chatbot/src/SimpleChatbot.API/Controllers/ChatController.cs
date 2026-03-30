using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using System.Threading.Tasks;

namespace SimpleChatbot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly OpenAIAPI _openAIAPI;

        public ChatController()
        {
            _openAIAPI = new OpenAIAPI("YOUR_API_KEY"); // Please replace with your OpenAI API Key
        }

        [HttpPost("send")]  // Endpoint to send a chat message
        public async Task<IActionResult> SendMessage([FromBody] string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return BadRequest("Message cannot be empty.");
            }

            var response = await _openAIAPI.Completions.CreateCompletionAsync(message);
            return Ok(response.Completions[0].Text.Trim());
        }

        [HttpGet("history")] // Endpoint to get chat history (implement this as per your needs)
        public IActionResult GetChatHistory()
        {
            // Example implementation (to be replaced with real logic).
            return Ok(new string[] { "Chat history feature not implemented yet." });
        }
    }
}