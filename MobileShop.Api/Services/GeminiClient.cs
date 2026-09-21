
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace MobileShop.Api.Services;

public class GeminiClient
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;
    private readonly ILogger<GeminiClient> _logger;

    public GeminiClient(
        HttpClient http,
        IConfiguration config,
        ILogger<GeminiClient> logger)
    {
        _http = http;
        _config = config;
        _logger = logger;
    }

    public bool IsConfigured =>
        !string.IsNullOrWhiteSpace(_config["Ai:ApiKey"]);

    public async Task<string> CompleteAsync(
        string systemPrompt,
        List<(string role, string content)> messages,
        int maxTokens = 500,
        CancellationToken cancellationToken = default)
    {
        var apiKey = _config["Ai:ApiKey"];
        var model = _config["Ai:Model"] ?? "gemini-3.8-flash";
        var baseUrl = _config["Ai:BaseUrl"]
            ?? "https://generativelanguage.googleapis.com/v1beta";

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new InvalidOperationException(
                "Chưa cấu hình Ai:ApiKey.");
        }

        var contents = messages
            .Where(m =>
                (m.role == "user" || m.role == "assistant") &&
                !string.IsNullOrWhiteSpace(m.content))
            .Select(m => new
            {
                role = m.role == "assistant" ? "model" : "user",
                parts = new[]
                {
                    new { text = m.content }
                }
            })
            .ToList();

        if (contents.Count == 0 ||
            contents[^1].role != "user")
        {
            throw new InvalidOperationException(
                "Hội thoại phải kết thúc bằng tin nhắn user.");
        }

        var payload = new
        {
            systemInstruction = new
            {
                parts = new[]
                {
                    new { text = systemPrompt }
                }
            },
            contents,
            generationConfig = new
            {
                maxOutputTokens = maxTokens,
                temperature = 0.7
            }
        };

        var endpoint =
            $"{baseUrl.TrimEnd('/')}/models/{model}:generateContent";

        using var request = new HttpRequestMessage(
            HttpMethod.Post, endpoint);

        request.Headers.Add("x-goog-api-key", apiKey);
        request.Content = JsonContent.Create(payload);

        using var response = await _http.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);

        var body = await response.Content.ReadAsStringAsync(
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning(
                "Gemini API trả về HTTP {StatusCode}",
                (int)response.StatusCode);

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                throw new HttpRequestException(
                    "Gemini đã đạt giới hạn sử dụng. Vui lòng thử lại sau.",
                    null,
                    response.StatusCode);
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized ||
                response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new HttpRequestException(
                    "Gemini API key không hợp lệ hoặc chưa được cấp quyền.",
                    null,
                    response.StatusCode);
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new HttpRequestException(
                    "Không tìm thấy model Gemini đã cấu hình.",
                    null,
                    response.StatusCode);
            }

            throw new HttpRequestException(
                $"Gemini API gặp lỗi HTTP {(int)response.StatusCode}.",
                null,
                response.StatusCode);
        }

        using var doc = JsonDocument.Parse(body);

        if (!doc.RootElement.TryGetProperty(
                "candidates", out var candidates) ||
            candidates.GetArrayLength() == 0)
        {
            throw new InvalidOperationException(
                "Gemini không trả về câu trả lời.");
        }

        var candidate = candidates[0];

        if (!candidate.TryGetProperty(
                "content", out var content) ||
            !content.TryGetProperty(
                "parts", out var parts))
        {
            throw new InvalidOperationException(
                "Phản hồi Gemini không có nội dung.");
        }

        var textParts = parts
            .EnumerateArray()
            .Where(p =>
                p.TryGetProperty("text", out _))
            .Select(p =>
                p.GetProperty("text").GetString() ?? "")
            .Where(t => !string.IsNullOrWhiteSpace(t));

        var result = string.Join("\n", textParts);

        if (string.IsNullOrWhiteSpace(result))
        {
            throw new InvalidOperationException(
                "Gemini trả về câu trả lời rỗng.");
        }

        return result;
    }
}