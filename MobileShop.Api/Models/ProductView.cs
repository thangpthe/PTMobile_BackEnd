namespace MobileShop.Api.Models
{
    // Lịch sử xem sản phẩm - dùng làm dữ liệu đầu vào cho AI gợi ý sản phẩm
    public class ProductView
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public DateTime ViewedAt { get; set; } = DateTime.UtcNow;
    }
}
