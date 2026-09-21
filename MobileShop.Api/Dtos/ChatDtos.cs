namespace MobileShop.Api.Dtos
{
    public record ChatRequest(string SessionId, string Message);
    public record ChatMessageDto(string Role, string Content, DateTime CreatedAt);
    public record ChatResponse(string Reply, List<ChatMessageDto> History);
}
