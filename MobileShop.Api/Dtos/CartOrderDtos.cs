namespace MobileShop.Api.Dtos
{
    public record AddToCartRequest(int ProductId, int Quantity);
    public record UpdateCartItemRequest(int Quantity);

    public record CartItemDto(int Id, int ProductId, string ProductName, string ImageUrl, decimal Price, int Quantity);

    public record CheckoutRequest(string ShippingAddress, string PhoneNumber);

    public record OrderItemDto(int ProductId, string ProductName, int Quantity, decimal UnitPrice);
    public record OrderDto(int Id, decimal TotalAmount, string Status, DateTime CreatedAt, List<OrderItemDto> Items);
}
