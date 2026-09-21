using System.ComponentModel.DataAnnotations.Schema;

namespace MobileShop.Api.Models
{
    public enum OrderStatus
    {
        Pending,
        Confirmed,
        Shipping,
        Completed,
        Cancelled
    }

    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        [Column(TypeName = "numeric(18,0)")]
        public decimal TotalAmount { get; set; }

        public string ShippingAddress { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }

    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }

        [Column(TypeName = "numeric(18,0)")]
        public decimal UnitPrice { get; set; } // giá tại thời điểm mua
    }
}
