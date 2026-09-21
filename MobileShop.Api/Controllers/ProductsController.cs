using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileShop.Api.Data;
using MobileShop.Api.Dtos;
using MobileShop.Api.Models;
using MobileShop.Api.Services;

namespace MobileShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly RecommendationService _recommendation;

        public ProductsController(AppDbContext db, RecommendationService recommendation)
        {
            _db = db;
            _recommendation = recommendation;
        }

        // GET /api/products?category=Mobile&search=iphone&brand=Apple
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAll(
            [FromQuery] string? category, [FromQuery] string? search, [FromQuery] string? brand)
        {
            var query = _db.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category) && Enum.TryParse<ProductCategory>(category, true, out var cat))
                query = query.Where(p => p.Category == cat);

            if (!string.IsNullOrWhiteSpace(brand))
                query = query.Where(p => p.Brand.ToLower() == brand.ToLower());

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(p => p.Name.ToLower().Contains(search.ToLower())
                                       || p.Description.ToLower().Contains(search.ToLower()));

            var products = await query.OrderByDescending(p => p.Id).ToListAsync();
            return Ok(products.Select(ProductDto.FromEntity));
        }

        // GET /api/products/5 -> đồng thời ghi nhận lượt xem cho AI recommendation
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();

            int? userId = GetUserIdOrNull();
            await _recommendation.RecordViewAsync(userId, id);

            return Ok(ProductDto.FromEntity(product));
        }

        [HttpGet("trending")]
        public async Task<ActionResult<List<ProductDto>>> GetTrending([FromQuery] int take = 8)
            => Ok(await _recommendation.GetTrendingAsync(take));

        [HttpGet("recommendations")]
        public async Task<ActionResult<List<ProductDto>>> GetRecommendations([FromQuery] int take = 8)
        {
            var userId = GetUserIdOrNull();
            return Ok(await _recommendation.GetAiRecommendationsAsync(userId, take));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductDto>> Create(ProductUpsertRequest req)
        {
            if (!Enum.TryParse<ProductCategory>(req.Category, true, out var category))
                return BadRequest("Loại sản phẩm không hợp lệ (Mobile | Tablet | Accessories).");

            var product = new Product
            {
                Name = req.Name,
                Description = req.Description,
                Brand = req.Brand,
                Price = req.Price,
                ImageUrl = req.ImageUrl,
                Category = category,
                StockQuantity = req.StockQuantity
            };
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return Ok(ProductDto.FromEntity(product));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductDto>> Update(int id, ProductUpsertRequest req)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();

            if (!Enum.TryParse<ProductCategory>(req.Category, true, out var category))
                return BadRequest("Loại sản phẩm không hợp lệ (Mobile | Tablet | Accessories).");

            product.Name = req.Name;
            product.Description = req.Description;
            product.Brand = req.Brand;
            product.Price = req.Price;
            product.ImageUrl = req.ImageUrl;
            product.Category = category;
            product.StockQuantity = req.StockQuantity;

            await _db.SaveChangesAsync();
            return Ok(ProductDto.FromEntity(product));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        private int? GetUserIdOrNull()
        {
            var claim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            return claim != null ? int.Parse(claim.Value) : null;
        }
    }
}
