using MobileShop.Api.Models;

namespace MobileShop.Api.Dtos
{
    public record ProductDto(
        int Id, string Name, string Description, string Brand,
        decimal Price, string ImageUrl, string Category,
        int SoldCount, double AverageRating, int ViewCount, int StockQuantity)
    {
        public static ProductDto FromEntity(Product p) => new(
            p.Id, p.Name, p.Description, p.Brand, p.Price, p.ImageUrl,
            p.Category.ToString(), p.SoldCount, p.AverageRating, p.ViewCount, p.StockQuantity);
    }

    public record ProductUpsertRequest(
        string Name, string Description, string Brand,
        decimal Price, string ImageUrl, string Category, int StockQuantity);
}
