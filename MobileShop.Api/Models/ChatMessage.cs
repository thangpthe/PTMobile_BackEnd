namespace MobileShop.Api.Models
{
    public enum ChatRole
    {
        User,
        Assistant
    }

    // Lịch sử hội thoại chatbot AI, lưu theo phiên (SessionId) để có ngữ cảnh
    public class ChatMessage
    {
        public int Id { get; set; }

        public int? UserId { get; set; } // null nếu khách chưa đăng nhập
        public User? User { get; set; }

        public string SessionId { get; set; } = string.Empty;

        public ChatRole Role { get; set; }

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
