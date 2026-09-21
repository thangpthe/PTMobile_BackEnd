using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileShop.Api.Models
{
    // Loại sản phẩm - giữ nguyên 3 nhóm như project gốc
    public enum ProductCategory
    {
        Mobile,
        Tablet,
        Accessories
    }

    public class Product
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty; // TenSP

        [Required]
        public string Description { get; set; } = string.Empty; // Mota

        [MaxLength(100)]
        public string Brand { get; set; } = string.Empty; // Hangsx

        [Column(TypeName = "numeric(18,0)")]
        public decimal Price { get; set; } // Giatien

        [MaxLength(500)]
        public string ImageUrl { get; set; } = string.Empty; // Anhsp

        public ProductCategory Category { get; set; } // Loaisp

        // Dùng để tính "sản phẩm được tin dùng"
        public int SoldCount { get; set; } = 0;
        public double AverageRating { get; set; } = 0;
        public int ViewCount { get; set; } = 0;

        public int StockQuantity { get; set; } = 100;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<ProductView> Views { get; set; } = new List<ProductView>();
    }
}
