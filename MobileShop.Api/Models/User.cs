using System.ComponentModel.DataAnnotations;

namespace MobileShop.Api.Models
{
    public enum UserRole
    {
        Customer,
        Admin
    }

    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string FullName { get; set; } = string.Empty; // HoTen

        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty; // Sdt

        [MaxLength(300)]
        public string Address { get; set; } = string.Empty; // DiaChi

        [Required, MaxLength(100)]
        public string Username { get; set; } = string.Empty; // TaiKhoan

        [Required]
        public string PasswordHash { get; set; } = string.Empty; // MatKhau (đã hash, không lưu plaintext)

        public UserRole Role { get; set; } = UserRole.Customer;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<ProductView> ProductViews { get; set; } = new List<ProductView>();
        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
    }
}
