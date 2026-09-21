using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MobileShop.Api.Dtos;
using MobileShop.Api.Services;

namespace MobileShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ChatbotService _chatbot;

        public ChatController(ChatbotService chatbot)
        {
            _chatbot = chatbot;
        }

        // Không bắt buộc đăng nhập - khách vãng lai vẫn chat được, chỉ cần sessionId (client tự sinh, ví dụ lưu trong localStorage)
        [HttpPost]
        public async Task<ActionResult<ChatResponse>> Send(ChatRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.SessionId) || string.IsNullOrWhiteSpace(req.Message))
                return BadRequest("Thiếu sessionId hoặc message.");

            int? userId = null;
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null) userId = int.Parse(claim.Value);

            var result = await _chatbot.SendMessageAsync(userId, req.SessionId, req.Message);
            return Ok(result);
        }
    }
}
