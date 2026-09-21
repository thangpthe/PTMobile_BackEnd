using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using MobileShop.Api.Data;
using MobileShop.Api.Dtos;
using MobileShop.Api.Models;

namespace MobileShop.Api.Services
{
    public class RecommendationService
    {
        private readonly AppDbContext _db;
        private readonly GeminiClient _ai;
        private readonly ILogger<RecommendationService> _logger;

        public RecommendationService(AppDbContext db, GeminiClient ai, ILogger<RecommendationService> logger)
        {
            _db = db;
            _ai = ai;
            _logger = logger;
        }

        /// <summary>
        /// Ghi nhận một lượt xem sản phẩm - dùng làm dữ liệu hành vi cho AI gợi ý.
        /// </summary>
        public async Task RecordViewAsync(int? userId, int productId)
        {
            var product = await _db.Products.FindAsync(productId);
            if (product == null) return;

            product.ViewCount += 1;

            if (userId.HasValue)
            {
                _db.ProductViews.Add(new ProductView { UserId = userId.Value, ProductId = productId });
            }
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Danh sách "sản phẩm được tin dùng" mặc định (không cá nhân hoá) dựa trên số liệu bán/rating -
        /// dùng làm fallback khi chưa có API key AI hoặc chưa có lịch sử người dùng.
        /// </summary>
        public async Task<List<ProductDto>> GetTrendingAsync(int take = 8)
        {
            var products = await _db.Products
                .OrderByDescending(p => p.SoldCount * 0.7 + p.AverageRating * 20 + p.ViewCount * 0.1)
                .Take(take)
                .ToListAsync();
            return products.Select(ProductDto.FromEntity).ToList();
        }

        /// <summary>
        /// Gợi ý sản phẩm cá nhân hoá bằng AI: dựa trên mô tả sản phẩm + lịch sử xem của user.
        /// Fallback về GetTrendingAsync nếu chưa cấu hình AI hoặc user chưa có lịch sử.
        /// </summary>
        public async Task<List<ProductDto>> GetAiRecommendationsAsync(int? userId, int take = 8)
        {
            if (!_ai.IsConfigured || !userId.HasValue)
            {
                return await GetTrendingAsync(take);
            }

            var viewedProductIds = await _db.ProductViews
                .Where(v => v.UserId == userId.Value)
                .OrderByDescending(v => v.ViewedAt)
                .Select(v => v.ProductId)
                .Distinct()
                .Take(10)
                .ToListAsync();

            var purchasedProductIds = await _db.OrderItems
                .Where(oi => oi.Order.UserId == userId.Value)
                .Select(oi => oi.ProductId)
                .Distinct()
                .ToListAsync();

            var historyIds = viewedProductIds.Union(purchasedProductIds).ToList();

            if (historyIds.Count == 0)
            {
                return await GetTrendingAsync(take);
            }

            var historyProducts = await _db.Products.Where(p => historyIds.Contains(p.Id)).ToListAsync();
            var candidateProducts = await _db.Products
                .Where(p => !historyIds.Contains(p.Id))
                .OrderByDescending(p => p.SoldCount)
                .Take(40) // giới hạn số ứng viên gửi cho AI để tiết kiệm token
                .ToListAsync();

            if (candidateProducts.Count == 0)
            {
                return await GetTrendingAsync(take);
            }

            var systemPrompt =
                "Bạn là hệ thống gợi ý sản phẩm cho cửa hàng công nghệ. " +
                "Dựa trên lịch sử xem/mua và danh sách ứng viên bên dưới, hãy chọn ra các sản phẩm phù hợp nhất để gợi ý tiếp cho khách. " +
                $"Chỉ trả lời DUY NHẤT một mảng JSON các id số nguyên, ví dụ: [12,5,7]. Không giải thích gì thêm. Chọn tối đa {take} id, ưu tiên sản phẩm cùng phân khúc/hãng/nhu cầu với lịch sử của khách nhưng đa dạng loại sản phẩm nếu hợp lý.";

            var historyText = string.Join("\n", historyProducts.Select(p =>
                $"- {p.Name} ({p.Category}, {p.Brand}, {p.Price:N0}đ): {p.Description}"));

            var candidateText = string.Join("\n", candidateProducts.Select(p =>
                $"id={p.Id} | {p.Name} ({p.Category}, {p.Brand}, {p.Price:N0}đ, đã bán {p.SoldCount}): {p.Description}"));

            var userPrompt =
                $"LỊCH SỬ XEM/MUA CỦA KHÁCH:\n{historyText}\n\nDANH SÁCH ỨNG VIÊN:\n{candidateText}\n\n" +
                "Trả lời bằng mảng JSON các id phù hợp nhất.";

            try
            {
                var raw = await _ai.CompleteAsync(systemPrompt, new List<(string, string)> { ("user", userPrompt) }, maxTokens: 300);
                var jsonStart = raw.IndexOf('[');
                var jsonEnd = raw.LastIndexOf(']');
                if (jsonStart == -1 || jsonEnd == -1 || jsonEnd < jsonStart)
                {
                    return await GetTrendingAsync(take);
                }
                var jsonArray = raw.Substring(jsonStart, jsonEnd - jsonStart + 1);
                var ids = JsonSerializer.Deserialize<List<int>>(jsonArray) ?? new List<int>();

                var recommended = candidateProducts.Where(p => ids.Contains(p.Id))
                    .OrderBy(p => ids.IndexOf(p.Id))
                    .Take(take)
                    .ToList();

                if (recommended.Count == 0)
                {
                    return await GetTrendingAsync(take);
                }

                return recommended.Select(ProductDto.FromEntity).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "AI recommendation thất bại, fallback sang trending.");
                return await GetTrendingAsync(take);
            }
        }
    }
}
