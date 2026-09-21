using MobileShop.Api.Models;

namespace MobileShop.Api.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext db)
        {
            db.Database.EnsureCreated();

            if (!db.Users.Any())
            {
                db.Users.AddRange(
                    new User
                    {
                        FullName = "Quản trị viên",
                        PhoneNumber = "0900000000",
                        Address = "Hà Nội",
                        Username = "admin",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                        Role = UserRole.Admin
                    },
                    new User
                    {
                        FullName = "Phùng Thế Thăng",
                        PhoneNumber = "0347826929",
                        Address = "Thanh Xuân",
                        Username = "thethang",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("thethang123"),
                        Role = UserRole.Customer
                    },
                    new User
                    {
                        FullName = "Vũ Văn Phúc",
                        PhoneNumber = "0123456789",
                        Address = "Định Công",
                        Username = "vanphuc",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("vanphuc123"),
                        Role = UserRole.Customer
                    }
                );
                db.SaveChanges();
            }

            if (!db.Products.Any())
            {
                db.Products.AddRange(ProductSeedData.GetProducts());
                db.SaveChanges();
            }
        }
    }
}
