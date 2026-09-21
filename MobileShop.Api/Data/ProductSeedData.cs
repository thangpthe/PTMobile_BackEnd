using MobileShop.Api.Models;

namespace MobileShop.Api.Data
{
    // Dữ liệu mẫu chuyển nguyên trạng từ project MobileShop gốc (Global.asax.cs)
    public static class ProductSeedData
    {
        public static List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product {
                    Name = "Samsung Galaxy S24 Ultra",
                    Description = "Màn hình 6.8 inch Dynamic AMOLED 2X tần số quét 120Hz. Máy cũng sở hữu camera chính 200MP, camera zoom quang học 50MP, camera tele 10MP và camera góc siêu rộng 12MP.",
                    Brand = "Samsung",
                    Price = 29990000m,
                    ImageUrl = "/images/Mobile/s24-ultra-vang_638409930027889246.png",
                    Category = ProductCategory.Mobile,
                    SoldCount = 342,
                    AverageRating = 3.9,
                    ViewCount = 502,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Xiaomi 14 Ultra",
                    Description = "Màn hình: Kích thước 6.73 inch, tấm nền AMOLED, độ phân giải 2K (3.200 x 1.440 pixels), tần số quét 120 Hz, độ sáng tối đa 3.000 nits. CPU: Snapdragon 8 Gen 3. RAM: 16 GB. Bộ nhớ trong: 512 GB.",
                    Brand = "Xiaomi",
                    Price = 29990000m,
                    ImageUrl = "/images/Mobile/xiaomi-14-ultra.jpg",
                    Category = ProductCategory.Mobile,
                    SoldCount = 140,
                    AverageRating = 4.1,
                    ViewCount = 212,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Oppo A78",
                    Description = "Màn hình: Kích thước 6.43 inch, tấm nền IPS LCD, độ phân giải Full HD+ (1.080 x 2.400 pixels), tần số quét 90 Hz. Vi xử lý: Snapdragon 680. RAM: 8 GB. Bộ nhớ trong: 256 GB, hỗ trợ mở rộng bộ nhớ qua thẻ nhớ MicroSD (tối đa 1 TB).",
                    Brand = "Oppo",
                    Price = 6990000m,
                    ImageUrl = "/images/Mobile/oppo-a78-xanh-thumb-1-600x600.jpg",
                    Category = ProductCategory.Mobile,
                    SoldCount = 361,
                    AverageRating = 4.7,
                    ViewCount = 660,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Samsung Galaxy A55",
                    Description = "Hiệu năng mạnh mẽ nhờ chip Exynos 1480 8 nhân, bên cạnh màn hình Super AMOLED cùng độ phân giải Full HD+ 1080 x 2400 cho trải nghiệm giải trí đỉnh cao",
                    Brand = "Samsung",
                    Price = 9690000m,
                    ImageUrl = "/images/Mobile/sp1.jpg",
                    Category = ProductCategory.Mobile,
                    SoldCount = 59,
                    AverageRating = 4.5,
                    ViewCount = 95,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Redmi Note 13",
                    Description = "Màn hình: Kích thước 6.67 inch, tấm nền AMOLED, độ phân giải Full HD+ (1.080 x 2.400 pixels), tần số quét 120 Hz. CPU: Snapdragon 685.",
                    Brand = "Xiaomi",
                    Price = 4390000m,
                    ImageUrl = "/images/Mobile/sp3.jpg",
                    Category = ProductCategory.Mobile,
                    SoldCount = 30,
                    AverageRating = 3.9,
                    ViewCount = 169,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Realme C67",
                    Description = "Chipset Snapdragon 685 6nm, dung lượng RAM 8GB, bộ nhớ trong 128 GB cùng viên pin Li-po 5000 mAh",
                    Brand = "Realme",
                    Price = 4390000m,
                    ImageUrl = "/images/Mobile/realme-c67.jpg",
                    Category = ProductCategory.Mobile,
                    SoldCount = 273,
                    AverageRating = 4.5,
                    ViewCount = 394,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Iphone 16 Pro",
                    Description = "Phone 16 Pro có thay đổi một chút về kích thước với màn hình 6.3 inch lớn hơn, viền mỏng hơn và lớp hoàn thiện bằng titan. Sự bổ sung các nâng cấp vượt trội về camera cũng là điểm nhấn với Apple Intelligence (AI) bao gồm nút Điều khiển Camera - Camera Control. Hiệu suất và tốc độ nhanh hơn đến 20% với chipset Apple A18 Pro mới của nhà sản xuất hàng đầu thế giới - TSMC.",
                    Brand = "Apple",
                    Price = 28990000m,
                    ImageUrl = "/images/Mobile/iphone16pro.jpeg",
                    Category = ProductCategory.Mobile,
                    SoldCount = 381,
                    AverageRating = 4.6,
                    ViewCount = 680,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Iphone 16 Plus",
                    Description = "IPhone 16 Plus được trang bị chip Apple A18 Bionic (3nm), mang lại hiệu năng mạnh mẽ, xử lý mượt mà mọi tác vụ từ đa nhiệm đến các ứng dụng nặng",
                    Brand = "Apple",
                    Price = 24990000m,
                    ImageUrl = "/images/Mobile/iphone-16-plus.jpg",
                    Category = ProductCategory.Mobile,
                    SoldCount = 229,
                    AverageRating = 4.1,
                    ViewCount = 391,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Iphone 15 Pro Max",
                    Description = "Chip A17 Bionic mạnh mẽ hơn 30% so với phiên bản tiền nhiệm, cùng RAM 8GB cho khả năng đa nhiệm tối ưu nhất. Pin dung lượng lớn hỗ trợ 95 giờ phát nhạc, giúp người dùng sử dụng trong cả ngày dài.",
                    Brand = "Apple",
                    Price = 26690000m,
                    ImageUrl = "/images/Mobile/15-pro-max-xanh-2.png",
                    Category = ProductCategory.Mobile,
                    SoldCount = 429,
                    AverageRating = 4.8,
                    ViewCount = 530,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Ipad Gen 9",
                    Description = "Màn hình Retina IPS LCD, có độ phân giải chuẩn 1620 x 2160 Pixel với tỷ lệ khung 4:3 và sở hữu tốc độ chạm là 60 Hz. Ngoài ra, cả mật độ điểm ảnh và độ sáng của iPad Gen 9 đều được nhận xét là vô cùng tốt, cụ thể là đạt chỉ số 264 ppi và 450 nits.",
                    Brand = "Apple",
                    Price = 6990000m,
                    ImageUrl = "/images/Tablet/ipadgen9.jpg",
                    Category = ProductCategory.Tablet,
                    SoldCount = 372,
                    AverageRating = 4.3,
                    ViewCount = 534,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Ipad Gen 10",
                    Description = "Trang bị màn hình Liquid Retina IPS, kích thước 10.9 inch, 2.360 x 1.640 pixel giúp hiển thị sắc nét và chi tiết hơn, trang bị công nghệ True Tone.",
                    Brand = "Apple",
                    Price = 13490000m,
                    ImageUrl = "/images/Tablet/ipad-gen-10-4.jpg",
                    Category = ProductCategory.Tablet,
                    SoldCount = 94,
                    AverageRating = 4.1,
                    ViewCount = 286,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Ipad Pro M4 11 inch",
                    Description = "iPad Pro M4 11 inch là mẫu máy tính bảng dành cho các công việc chuyên nghiệp khi được trang bị con chip M4 với hiệu năng vượt bậc. Thế hệ iPad Pro mới còn sở hữu thiết kế mới mảnh mai hơn cùng màn hình Ultra Retina XDR siêu đẹp mắt để nâng tầm trải nghiệm.",
                    Brand = "Apple",
                    Price = 6990000m,
                    ImageUrl = "/images/Tablet/ipad-pro-13.jpg",
                    Category = ProductCategory.Tablet,
                    SoldCount = 67,
                    AverageRating = 3.9,
                    ViewCount = 136,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Samsung Galaxy Tab A7",
                    Description = "Bên trong Samsung Galaxy Tab A7 (2020) là bộ vi xử lý Snapdragon 662 gồm 4 lõi 2.0 GHz và 4 lỗi 1.8 Ghz được sản xuất theo tiến trình 11 nm mang đến hiệu năng ổn định, đảm bảo các tác vụ luôn được xử lý một cách mượt mà, hiếm khi xảy ra hiện tượng giật lag.\\r\\n\\r\\n",
                    Brand = "Samsung",
                    Price = 3790000m,
                    ImageUrl = "/images/Tablet/samsung-galaxy-tab-a7.jpg",
                    Category = ProductCategory.Tablet,
                    SoldCount = 198,
                    AverageRating = 4.8,
                    ViewCount = 353,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Lenovo Tab M11",
                    Description = "Lenovo Tab M11 sở hữu cho mình màn hình IPS LCD với kích thước lớn lên đến 11 inch, độ phân giải 1.920 x 1.200 pixels, tấm nền được trang bị trên Tab M11 còn hỗ trợ tần số quét 90 Hz.",
                    Brand = "Lenovo",
                    Price = 5090000m,
                    ImageUrl = "/images/Tablet/sp5.png",
                    Category = ProductCategory.Tablet,
                    SoldCount = 428,
                    AverageRating = 3.9,
                    ViewCount = 683,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Xiaomi Pad 5 Pro",
                    Description = "Xiaomi Pad 5 Pro sở hữu chip Snapdragon 870 5G cực mạnh, màn hình IPS LCD độ phân giải QHD+ (2K+), tần số quét 120Hz siêu mượt. Cung cấp năng lượng cho máy hoạt động là viên pin 8600mAh kèm sạc siêu nhanh 67W",
                    Brand = "Xiaomi",
                    Price = 8690000m,
                    ImageUrl = "/images/Tablet/xiao-pad-5-pro.jpg",
                    Category = ProductCategory.Tablet,
                    SoldCount = 289,
                    AverageRating = 3.9,
                    ViewCount = 502,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Xiaomi Pad 6",
                    Description = "Hiệu năng siêu mạnh với Snapdragon 8+ Gen 1, màn hình 11 inch, độ phân giải 2.8K với tần số quét 144Hz, viên pin lớn 8600mAh kèm sạc nhanh 67W cũng như camera kép 50MP chất lượng",
                    Brand = "Xiaomi",
                    Price = 8490000m,
                    ImageUrl = "/images/Tablet/xiaomi-pad-6.jpg",
                    Category = ProductCategory.Tablet,
                    SoldCount = 55,
                    AverageRating = 4.5,
                    ViewCount = 260,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Xiaomi Pad SE",
                    Description = "Hiệu năng siêu mạnh với Snapdragon 8+ Gen 1, màn hình 11 inch, độ phân giải 2.8K với tần số quét 144Hz, viên pin lớn 8600mAh kèm sạc nhanh 67W cũng như camera kép 50MP chất lượng",
                    Brand = "Xiaomi",
                    Price = 4390000m,
                    ImageUrl = "/images/Tablet/xiaomi-pad-se.jpg",
                    Category = ProductCategory.Tablet,
                    SoldCount = 310,
                    AverageRating = 4.0,
                    ViewCount = 365,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Samsung Galaxy Tab S9",
                    Description = "Với vi xử lý Qualcomm Snapdragon 8nm và RAM 8GB, Samsung Galaxy Tab S9 128GB mang đến khả năng xử lý mạnh mẽ và tốc độ chưa từng có. Điều này cho phép bạn trải nghiệm mượt mà các ứng dụng, trò chơi, và dễ dàng chuyển đổi giữa các tác vụ mà không hề bị gián đoạn.",
                    Brand = "Samsung",
                    Price = 16990000m,
                    ImageUrl = "/images/Tablet/samsung-galaxy-tab-s9.png",
                    Category = ProductCategory.Tablet,
                    SoldCount = 38,
                    AverageRating = 4.6,
                    ViewCount = 206,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Giá treo màn hình",
                    Description = "Giá treo màn hình North Bayou M2 Màu xám là giải pháp hoàn hảo cho những ai muốn tối ưu hóa không gian làm việc và nâng cao trải nghiệm sử dụng màn hình. Với thiết kế hiện đại và tính năng linh hoạt, sản phẩm này giúp bạn dễ dàng điều chỉnh vị trí của màn hình để đạt được góc nhìn tối ưu.",
                    Brand = "North Bayou",
                    Price = 349000m,
                    ImageUrl = "/images/Accessories/66681_gia_treo_man_hinh_north_bayou_g40.jpg",
                    Category = ProductCategory.Accessories,
                    SoldCount = 55,
                    AverageRating = 4.8,
                    ViewCount = 126,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Ốp lưng Magsafe",
                    Description = "Được chế tạo từ hỗn hợp chất liệu polycarbonate trong suốt và chất liệu dẻo, ốp lưng vừa khít với các nút bấm của điện thoại, vô cùng tiện dụng. Ốp lưng này phối hợp mượt mà với Điều Khiển Camera. Ốp có mặt tinh thể sapphire, kết hợp với lớp dẫn truyền để chuyển động của ngón tay bạn có thể truyền qua Điều Khiển Camera.",
                    Brand = "Iphone",
                    Price = 490000m,
                    ImageUrl = "/images/Accessories/op-lung-magsafe-iphone-15-pro-max-nhua-trong-apple-mt233-thumb-650x650.png",
                    Category = ProductCategory.Accessories,
                    SoldCount = 209,
                    AverageRating = 4.1,
                    ViewCount = 415,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Sạc dự phòng",
                    Description = "Sạc dự phòng Energizer 10,000mAh - UE10053 chính hãng có thiết kế khá mỏng và nhẹ. Nó có kích thước khoảng 140 x 68 x 16 mm và trọng lượng chỉ khoảng 218g. Nhờ đó, bạn có thể dễ dàng bỏ vào túi và mang theo khi di chuyển bên ngoài.",
                    Brand = "Energizer",
                    Price = 299000m,
                    ImageUrl = "/images/Accessories/sacduphong.jpg",
                    Category = ProductCategory.Accessories,
                    SoldCount = 98,
                    AverageRating = 4.2,
                    ViewCount = 225,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Sạc nhanh 15W",
                    Description = "Sạc nhanh Samsung 15W type C được tích hợp với các công nghệ hiện đại để đảm bảo an toàn tối đa và giảm thiểu thời gian chờ đợi. Bên cạnh đó, việc tích hợp cáp USB-C cũng giúp người dùng dễ dàng kết nối với điện thoại hoặc máy tính bảng.",
                    Brand = "Apple",
                    Price = 310000m,
                    ImageUrl = "/images/Accessories/sacnhanh15w.jpg",
                    Category = ProductCategory.Accessories,
                    SoldCount = 358,
                    AverageRating = 4.1,
                    ViewCount = 414,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Bao da Ipad Gen 9",
                    Description = "Bao da cho iPad Gen 9 10.2 Mutural Design được hoàn thiện vô cùng độc đáo. Bên ngoài là chất liệu da tổng hợp. Chất liệu da này sẽ giúp mang đến vẻ ngoài thời thượng, giống hệt da thật. Do đó, không những nó mang lại vẻ sang trọng, tinh tế mà còn đem đến khả năng chống sốc, trầy xước rất tốt.",
                    Brand = "Apple",
                    Price = 380000m,
                    ImageUrl = "/images/Accessories/bao-da-ipad-gen-9.jpeg",
                    Category = ProductCategory.Accessories,
                    SoldCount = 326,
                    AverageRating = 4.6,
                    ViewCount = 619,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Camera Tapo C200",
                    Description = "Camera Tapo C200 được thiết kế trụ tròn quen thuộc với phần chân đế chắc chắn. Nó có kích thước cực nhỏ gọn để bạn có thể đặt ở bất cứ vị trí nào trong ngôi nhà của mình mà không lo chiếm diện tích.",
                    Brand = "TP-Link",
                    Price = 470000m,
                    ImageUrl = "/images/Accessories/Camera IP Wi-Fi TP-Link Tapo C212 3M.png",
                    Category = ProductCategory.Accessories,
                    SoldCount = 388,
                    AverageRating = 4.1,
                    ViewCount = 644,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Tay cầm chống rung DJI Osmo Mobile 4",
                    Description = "Thiết bị này chỉ có trọng lượng khoảng 390g, nhẹ hơn một chút so với thế hệ cũ, tất nhiên rồi. Điều này giúp người dùng có thể dễ dàng cầm nắm và thao tác cùng chiêc tay cầm chống rung mà không có cảm giác nặng tay khi sử dụng liên tục trong một thời gian dài. DJI Osmo Mobile 4 chính hãng được giữ nguyên các nút bấm điều chỉnh không có thay đổi nhiều.",
                    Brand = "DJI",
                    Price = 2890000m,
                    ImageUrl = "/images/Accessories/tay-cam-chong-rung.jpg",
                    Category = ProductCategory.Accessories,
                    SoldCount = 209,
                    AverageRating = 4.1,
                    ViewCount = 341,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Pin XS Max Pisen",
                    Description = "Với dung lượng lên tới 3450mAh, viên pin PISEN này cung cấp năng lượng dồi dào, giúp bạn sử dụng điện thoại trong thời gian dài mà không cần lo lắng về việc sạc lại thường xuyên..",
                    Brand = "Apple",
                    Price = 890000m,
                    ImageUrl = "/images/Accessories/pin-xs-max.png",
                    Category = ProductCategory.Accessories,
                    SoldCount = 365,
                    AverageRating = 4.2,
                    ViewCount = 413,
                    StockQuantity = 50
                },
                new Product {
                    Name = "Dây đeo Apple Air Tag",
                    Description = "Dây đeo AirTag Loop là giải pháp sử dụng và bảo vệ AirTag hiệu quả từ chính Apple. Với thiết kế thân thiện, bền bỉ và nhiều màu sắc đa dạng khác nhau, chiếc dây deo này sẽ hỗ trợ bạn treo AirTag lên các đồ vật dễ thất lạc để định vị bất cứ khi nào bạn cần.\\r\\n\\r\\n",
                    Brand = "Apple",
                    Price = 50000m,
                    ImageUrl = "/images/Accessories/apple-air-tag.jpg",
                    Category = ProductCategory.Accessories,
                    SoldCount = 132,
                    AverageRating = 4.8,
                    ViewCount = 313,
                    StockQuantity = 50
                },
            };
        }
    }
}