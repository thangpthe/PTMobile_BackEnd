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
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CartController(AppDbContext db)
        {
            _db = db;
        }

        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public async Task<ActionResult<List<CartItemDto>>> GetCart()
        {
            var items = await _db.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == UserId)
                .Select(c => new CartItemDto(c.Id, c.ProductId, c.Product.Name, c.Product.ImageUrl, c.Product.Price, c.Quantity))
                .ToListAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<CartItemDto>> AddToCart(AddToCartRequest req)
        {
            var product = await _db.Products.FindAsync(req.ProductId);
            if (product == null) return NotFound("Sản phẩm không tồn tại.");

            var existing = await _db.CartItems.Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.UserId == UserId && c.ProductId == req.ProductId);

            if (existing != null)
            {
                existing.Quantity += req.Quantity;
            }
            else
            {
                existing = new CartItem { UserId = UserId, ProductId = req.ProductId, Quantity = req.Quantity, Product = product };
                _db.CartItems.Add(existing);
            }
            await _db.SaveChangesAsync();

            return Ok(new CartItemDto(existing.Id, product.Id, product.Name, product.ImageUrl, product.Price, existing.Quantity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuantity(int id, UpdateCartItemRequest req)
        {
            var item = await _db.CartItems.FirstOrDefaultAsync(c => c.Id == id && c.UserId == UserId);
            if (item == null) return NotFound();

            if (req.Quantity <= 0)
            {
                _db.CartItems.Remove(item);
            }
            else
            {
                item.Quantity = req.Quantity;
            }
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var item = await _db.CartItems.FirstOrDefaultAsync(c => c.Id == id && c.UserId == UserId);
            if (item == null) return NotFound();

            _db.CartItems.Remove(item);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
