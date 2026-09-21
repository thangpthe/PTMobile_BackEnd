using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MobileShop.Api.Services
{
    /// <summary>
    /// Client gọi Anthropic Messages API (https://api.anthropic.com/v1/messages).
    /// Dùng chung cho cả tính năng Chatbot tư vấn và AI gợi ý sản phẩm.
    /// Có thể thay bằng OpenAI hoặc provider khác bằng cách sửa lại class này.
    /// </summary>
    public class AnthropicClient
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;
        private readonly ILogger<AnthropicClient> _logger;

        public AnthropicClient(HttpClient http, IConfiguration config, ILogger<AnthropicClient> logger)
        {
            _http = http;
            _config = config;
            _logger = logger;
        }

        public bool IsConfigured => !string.IsNullOrWhiteSpace(_config["Ai:ApiKey"]);

        /// <summary>
        /// Gửi 1 request text-only tới Claude, trả về nội dung text trả lời.
        /// messages: danh sách (role, content) theo thứ tự hội thoại, role là "user" hoặc "assistant".
        /// </summary>
        public async Task<string> CompleteAsync(string systemPrompt, List<(string role, string content)> messages, int maxTokens = 1024)
        {
            var apiKey = _config["Ai:ApiKey"];
            var model = _config["Ai:Model"] ?? "claude-sonnet-4-6";
            var baseUrl = _config["Ai:BaseUrl"] ?? "https://api.anthropic.com/v1/messages";

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new InvalidOperationException("Chưa cấu hình Ai:ApiKey trong appsettings.json / biến môi trường.");
            }

            var payload = new
            {
                model,
                max_tokens = maxTokens,
                system = systemPrompt,
                messages = messages.Select(m => new { role = m.role, content = m.content })
            };

            using var request = new HttpRequestMessage(HttpMethod.Post, baseUrl);
            request.Headers.Add("x-api-key", apiKey);
            request.Headers.Add("anthropic-version", "2023-06-01");
            request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await _http.SendAsync(request);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Anthropic API lỗi {Status}: {Body}", response.StatusCode, body);
                throw new HttpRequestException($"Anthropic API trả về lỗi {response.StatusCode}");
            }

            using var doc = JsonDocument.Parse(body);
            var textParts = new List<string>();
            foreach (var block in doc.RootElement.GetProperty("content").EnumerateArray())
            {
                if (block.GetProperty("type").GetString() == "text")
                {
                    textParts.Add(block.GetProperty("text").GetString() ?? "");
                }
            }
            return string.Join("\n", textParts);
        }
    }
}
