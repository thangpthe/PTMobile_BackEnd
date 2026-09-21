namespace MobileShop.Api.Dtos
{
    public record RegisterRequest(string FullName, string PhoneNumber, string Address, string Username, string Password);
    public record LoginRequest(string Username, string Password);
    public record AuthResponse(string Token, string Username, string FullName, string Role);
}
