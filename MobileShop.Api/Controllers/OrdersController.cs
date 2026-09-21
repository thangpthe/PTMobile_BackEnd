using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileShop.Api.Data;
using MobileShop.Api.Dtos;
using MobileShop.Api.Models;

namespace MobileShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _db;

        public OrdersController(AppDbContext db)
        {
            _db = db;
        }

        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetMyOrders()
        {
            var orders = await _db.Orders
                .Include(o => o.Items).ThenInclude(i => i.Product)
                .Where(o => o.UserId == UserId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            return Ok(orders.Select(o => new OrderDto(
                o.Id, o.TotalAmount, o.Status.ToString(), o.CreatedAt,
                o.Items.Select(i => new OrderItemDto(i.ProductId, i.Product.Name, i.Quantity, i.UnitPrice)).ToList())));
        }

        // Thanh toán giỏ hàng hiện tại -> tạo Order, trừ kho, cộng SoldCount (dùng cho AI "được tin dùng"), xoá giỏ hàng
        [HttpPost("checkout")]
        public async Task<ActionResult<OrderDto>> Checkout(CheckoutRequest req)
        {
            var cartItems = await _db.CartItems.Include(c => c.Product)
                .Where(c => c.UserId == UserId).ToListAsync();

            if (cartItems.Count == 0) return BadRequest("Giỏ hàng đang trống.");

            var order = new Order
            {
                UserId = UserId,
                ShippingAddress = req.ShippingAddress,
                PhoneNumber = req.PhoneNumber,
                Status = OrderStatus.Pending,
                TotalAmount = cartItems.Sum(c => c.Product.Price * c.Quantity)
            };

            foreach (var item in cartItems)
            {
                order.Items.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                });

                item.Product.SoldCount += item.Quantity;
                item.Product.StockQuantity = Math.Max(0, item.Product.StockQuantity - item.Quantity);
            }

            _db.Orders.Add(order);
            _db.CartItems.RemoveRange(cartItems);
            await _db.SaveChangesAsync();

            return Ok(new OrderDto(
                order.Id, order.TotalAmount, order.Status.ToString(), order.CreatedAt,
                order.Items.Select(i => new OrderItemDto(i.ProductId,
                    cartItems.First(c => c.ProductId == i.ProductId).Product.Name, i.Quantity, i.UnitPrice)).ToList()));
        }
    }
}
