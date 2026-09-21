namespace MobileShop.Api.Models
{
    // Giỏ hàng lưu theo user trong DB (thay cho cookie như bản gốc)
    public class CartItem
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; } = 1;

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
