using Microsoft.EntityFrameworkCore;
using TeaManager.API.Models.Domain;


namespace TeaManager.API.Data
{
    public class TeaManagerDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SupplierOrder> SupplierOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<SupplierOrder>()
                .HasOne(so => so.Supplier)
                .WithMany(s => s.SupplierOrders)
                .HasForeignKey(so => so.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SupplierOrder>()
                .HasOne(so => so.Product)
                .WithMany(p => p.SupplierOrders)
                .HasForeignKey(so => so.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            //Seed Data: Brands 
            modelBuilder.Entity<Brand>().HasData(
                new Brand
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    BrandId = 1,
                    BrandName = "Lipston",
                    Country = "United Kingdom",
                    FoundedYear = 1871,
                    Email = "info@lipston.com",
                    Phone = "+44 5-1234-56344",
                    Address = "Lipston House, 42-43 Park Street, London, UK",
                    BusinessRegNumber = "BRN123445",
                    OwnerName = "Jorge Russell",
                    CreatedAt = new DateTime(2024, 12, 10)
                },
                new Brand
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    BrandId = 2,
                    BrandName = "TeaCoo",
                    Country = "India",
                    FoundedYear = 1950,
                    Email = "info@teacoo.com",
                    Phone = "+91 12-2234-56342",
                    Address = "TeaCoo Plaza, 343 MG Road, Mumbai, India",
                    BusinessRegNumber = "BRN883456",
                    OwnerName = "Priya Sundar",
                    CreatedAt = new DateTime(2020, 6, 15)

                },
                new Brand
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    BrandId = 3,
                    BrandName = "LaobanTea",
                    Country = "China",
                    FoundedYear = 1855,
                    Email = "info@LaobanTea.com",
                    Phone = "+86 18-7533-34256",
                    Address = "LaobanTea Firm, 57 Tea Street, Pu'er City, Yunnan Province, China",
                    BusinessRegNumber = "BRN323595",
                    OwnerName = "Li Wei",
                    CreatedAt = new DateTime(2019, 12, 23)

                },
                new Brand
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000004"),
                    BrandId = 4,
                    BrandName = "NortLeaf",
                    Country = "Sri Lanka",
                    FoundedYear = 1950,
                    Email = "info@NortLeaf.com",
                    Phone = "+94 9384-32848",
                    Address = "NortLeaf Plaza, 343 MG Road, Colombo, Sri Lanka",
                    BusinessRegNumber = "BRN833626",
                    OwnerName = "Suan Perera",
                    CreatedAt = new DateTime(2021, 2, 13)

                },
                new Brand
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000005"),
                    BrandId = 5,
                    BrandName = "HealthTea",
                    Country = "United States",
                    FoundedYear = 2001,
                    Email = "info@HealthTea.com",
                    Phone = "+1 555-123-4567",
                    Address = "HealthTea Building, 123 Wellness Avenue, New York, USA",
                    BusinessRegNumber = "BRN994567",
                    OwnerName = "Sarah Johnson",
                    CreatedAt = new DateTime(2022, 3, 15)

                },
                new Brand
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000006"),
                    BrandId = 6,
                    BrandName = "HomeTea",
                    Country = "Ireland",
                    FoundedYear = 1900,
                    Email = "info@Hometea.com",
                    Phone = "+353 1-234-434256",
                    Address = "HomeTea House, 456 Tea Lane, Dublin, Ireland",
                    BusinessRegNumber = "BRN894567",
                    OwnerName = "Sam Smith",
                    CreatedAt = new DateTime(2022, 6, 15)

                },
                new Brand
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000007"),
                    BrandId = 7,
                    BrandName = "TeaFeast",
                    Country = "Malaysia",
                    FoundedYear = 1950,
                    Email = "info@Teafeast.com",
                    Phone = "+60 12-345-6789",
                    Address = "TeaFeast Plaza, 343 MG Road, Kuala Lumpur, Malaysia",
                    BusinessRegNumber = "BRN827456",
                    OwnerName = "Ahmad Sandar",
                    CreatedAt = new DateTime(2021, 7, 8)

                },
                new Brand
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000008"),
                    BrandId = 8,
                    BrandName = "TeaForest",
                    Country = "Kenya",
                    FoundedYear = 1950,
                    Email = "info@TeaForest.com",
                    Phone = "+254 12-365-8890",
                    Address = "TeaForest Plaza, 343 MG Road, Nairobi, Kenya",
                    BusinessRegNumber = "BRN883204",
                    OwnerName = "Samuel Harper",
                    CreatedAt = new DateTime(2023, 10, 15)

                },
                new Brand
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000009"),
                    BrandId = 9,
                    BrandName = "SoftTeabank",
                    Country = "Canada",
                    FoundedYear = 1999,
                    Email = "info@softteabank.com",
                    Phone = "+1 345-763-8901",
                    Address = "SoftTeabank Building, 123 Financial Avenue, Toronto, Canada",
                    BusinessRegNumber = "BRN884446",
                    OwnerName = "Emily Davis",
                    CreatedAt = new DateTime(2024, 8, 25)

                },
                new Brand
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000010"),
                    BrandId = 10,
                    BrandName = "TeaSeason",
                    Country = "Australia",
                    FoundedYear = 1999,
                    Email = "info@TeaSeason.com",
                    Phone = "+61 2-1234-5678",
                    Address = "TeaSeason Building, 456 Tea Street, Sydney, Australia",
                    BusinessRegNumber = "BRN885557",
                    OwnerName = "Michael Johnson",
                    CreatedAt = new DateTime(2023, 3, 14)

                }

            );
            //Seed Data: Suppliers
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000011"),
                    SupplierId = 1,
                    SupplierName = "Lipston Supplier",
                    Country = "United Kingdom",
                    ContactEmail = "supply@lipston.com",
                    Phone = "+44 6-2346-83745",
                    CreatedAt = new DateTime(2024, 12, 10)
                },
                 new Supplier
                 {
                     Id = new Guid("00000000-0000-0000-0000-000000000012"),
                     SupplierId = 2,
                     SupplierName = "Kingstone Supplier",
                     Country = "India",
                     ContactEmail = "supply@kingstone.com",
                     Phone = "+91 12-3334-55384",
                     CreatedAt = new DateTime(2020, 6, 15)
                 },
                 new Supplier
                 {
                     Id = new Guid("00000000-0000-0000-0000-000000000013"),
                     SupplierId = 3,
                     SupplierName = "LaobanTeaHub Supplier",
                     Country = "China",
                     ContactEmail = "supply@LaobanTeaHub.com",
                     Phone = "+86 18-7532-34253",
                     CreatedAt = new DateTime(2019, 12, 23)
                 },
                 new Supplier
                 {
                     Id = new Guid("00000000-0000-0000-0000-000000000014"),
                     SupplierId = 4,
                     SupplierName = "AloneTea Supplier",
                     Country = "Sri Lanka",
                     ContactEmail = "supply@alonetea.com",
                     Phone = "+94 90-8394-23456",
                     CreatedAt = new DateTime(2021, 2, 13)
                 },
                 new Supplier
                 {
                     Id = new Guid("00000000-0000-0000-0000-000000000015"),
                     SupplierId = 5,
                     SupplierName = "TrolTrol Supplier",
                     Country = "Sri Lanka",
                     ContactEmail = "supply@TrolTrol.com",
                     Phone = "+94 11-234-5678",
                     CreatedAt = new DateTime(2022, 3, 15)
                 },
                 new Supplier
                 {
                     Id = new Guid("00000000-0000-0000-0000-000000000016"),
                     SupplierId = 6,
                     SupplierName = "HalTea Supplier",
                     Country = "Kenya",
                     ContactEmail = "supply@HalTea.com",
                     Phone = "+254 11-234-5678",
                     CreatedAt = new DateTime(2022, 6, 15)
                 },
                 new Supplier
                 {
                     Id = new Guid("00000000-0000-0000-0000-000000000017"),
                     SupplierId = 7,
                     SupplierName = "Thia Supplier",
                     Country = "Thailand",
                     ContactEmail = "supply@Thia.com",
                     Phone = "+66 2-123-4567",
                     CreatedAt = new DateTime(2021, 7, 8)
                 },
                 new Supplier
                 {
                     Id = new Guid("00000000-0000-0000-0000-000000000018"),
                     SupplierId = 8,
                     SupplierName = "Funck Supplier",
                     Country = "Germany",
                     ContactEmail = "supply@Funck.com",
                     Phone = "+49 30-4568-2345",
                     CreatedAt = new DateTime(2023, 10, 15)
                 },
                 new Supplier
                 {
                     Id = new Guid("00000000-0000-0000-0000-000000000019"),
                     SupplierId = 9,
                     SupplierName = "OolongTeaHub Supplier",
                     Country = "China",
                     ContactEmail = "supply@OolongTeaHub.com",
                     Phone = "+86 10-1234-5678",
                     CreatedAt = new DateTime(2024, 8, 25)
                 },
                 new Supplier
                 {
                     Id = new Guid("00000000-0000-0000-0000-000000000020"),
                     SupplierId = 10,
                     SupplierName = "EdibleTea Supplier",
                     Country = "Japan",
                     ContactEmail = "supply@EdibleTea.com",
                     Phone = "+81 3-1234-5678",
                     CreatedAt = new DateTime(2023, 3, 14)
                 }
            );

            //Seed Data: Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000021"),
                    ProductId = 1,
                    ProductName = "Lipston Fresh Green Tea",
                    Description = "A refreshing green tea with a crisp flavor and various health benefits.",
                    Price = 18.59m,
                    Stock = 80,
                    HarvestYear = 2022,
                    Origin = "Cornwell,United Kingdom",
                    BrandId = new Guid("00000000-0000-0000-0000-000000000001"),
                    SupplierId = new Guid("00000000-0000-0000-0000-000000000011"),
                    CreatedAt = new DateTime(2022, 12, 10)
                },

                new Product
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000022"),
                    ProductId = 2,
                    ProductName = "TeaCoo Assam Black Tea",
                    Description = "A flavorful black tea from the Assam region of India.",
                    Price = 20.88m,
                    Stock = 80,
                    HarvestYear = 2020,
                    Origin = "Assam,India",
                    BrandId = new Guid("00000000-0000-0000-0000-000000000002"),
                    SupplierId = new Guid("00000000-0000-0000-0000-000000000012"),
                    CreatedAt = new DateTime(2020, 6, 15)
                },
                new Product
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000023"),
                    ProductId = 3,
                    ProductName = "LaobanTea Pu'erh Tea",
                    Description = "A rich and earthy tea from the Yunnan province of China, famous for its unique fermentation process.",
                    Price = 30.24m,
                    Stock = 20,
                    HarvestYear = 2019,
                    Origin = "Yunnan,China",
                    BrandId = new Guid("00000000-0000-0000-0000-000000000003"),
                    SupplierId = new Guid("00000000-0000-0000-0000-000000000013"),
                    CreatedAt = new DateTime(2019, 12, 23)
                },
                new Product
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000024"),
                    ProductId = 4,
                    ProductName = "NortLeaf Nuwera Premium Black Tea",
                    Description = "A flavorful black tea from the Nuwera Eliya region of Sri Lanka.",
                    Price = 50.00m,
                    Stock = 20,
                    HarvestYear = 2021,
                    Origin = "Nuwera Eliya,Sri Lanka",
                    BrandId = new Guid("00000000-0000-0000-0000-000000000004"),
                    SupplierId = new Guid("00000000-0000-0000-0000-000000000014"),
                    CreatedAt = new DateTime(2021, 2, 13)
                },

                new Product
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000025"),
                    ProductId = 5,
                    ProductName = "HealthTea Organic Mauna Kea Tea",
                    Description = "An organic tea blend from the Mauna Kea region of Hawaii, beautifully balanced with floral and earthy notes.",
                    Price = 25.45m,
                    Stock = 80,
                    HarvestYear = 2022,
                    Origin = "Hawaii,United States",
                    BrandId = new Guid("00000000-0000-0000-0000-000000000005"),
                    SupplierId = new Guid("00000000-0000-0000-0000-000000000015"),
                    CreatedAt = new DateTime(2022, 3, 15)
                },
                new Product
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000026"),
                    ProductId = 6,
                    ProductName = "HomeTea Irish Breakfast Tea",
                    Description = "Fresh morning tea with a clear and flavorful taste, perfect for starting your day.",
                    Price = 19.59m,
                    Stock = 150,
                    HarvestYear = 2022,
                    Origin = "Dublin,Ireland",
                    BrandId = new Guid("00000000-0000-0000-0000-000000000006"),
                    SupplierId = new Guid("00000000-0000-0000-0000-000000000016"),
                    CreatedAt = new DateTime(2022, 6, 15)
                },
                new Product
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000027"),
                    ProductId = 7,
                    ProductName = "TeaFeast Earl Grey Tea",
                    Description = "Premium earl grey tea with a refreshing citrus flavor and a hint of bergamot, perfect for tea lovers.",
                    Price = 15.59m,
                    Stock = 95,
                    HarvestYear = 2021,
                    Origin = "Chiang Mai,Thailand",
                    BrandId = new Guid("00000000-0000-0000-0000-000000000007"),
                    SupplierId = new Guid("00000000-0000-0000-0000-000000000017"),
                    CreatedAt = new DateTime(2021, 7, 8)
                },
                new Product
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000028"),
                    ProductId = 8,
                    ProductName = "TeaForest Ostfriesentee",
                    Description = "Blend of fabulous teas from Ostfriesland, Germany.",
                    Price = 18.95m,
                    Stock = 80,
                    HarvestYear = 2023,
                    Origin = "Ostfriesland,Germany",
                    BrandId = new Guid("00000000-0000-0000-0000-000000000008"),
                    SupplierId = new Guid("00000000-0000-0000-0000-000000000018"),
                    CreatedAt = new DateTime(2023, 10, 15)
                },
                new Product
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000029"),
                    ProductId = 9,
                    ProductName = "SoftTeabank Premium Yunnan Oolong Tea",
                    Description = "High-quality oolong tea from Yunnan, China.",
                    Price = 28.66m,
                    Stock = 95,
                    HarvestYear = 2024,
                    Origin = "Yunnan,China",
                    BrandId = new Guid("00000000-0000-0000-0000-000000000009"),
                    SupplierId = new Guid("00000000-0000-0000-0000-000000000019"),
                    CreatedAt = new DateTime(2024, 8, 25)
                },
                new Product
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000030"),
                    ProductId = 10,
                    ProductName = "TeaSeason Sencha Green Tea",
                    Description = "Fantastic sencha green tea from Japan, with a fresh and grassy flavor.",
                    Price = 33.59m,
                    Stock = 80,
                    HarvestYear = 2024,
                    Origin = "Shizuoka,Japan",
                    BrandId = new Guid("00000000-0000-0000-0000-000000000010"),
                    SupplierId = new Guid("00000000-0000-0000-0000-000000000020"),
                    CreatedAt = new DateTime(2023, 3, 14)
                }
            );


            //Seed Data: SupplierOrders
            modelBuilder.Entity<SupplierOrder>().HasData(
                new SupplierOrder
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000031"),
                    SupplierOrderId = 1,
                    Quantity = 100,
                    OrderDate = new DateTime(2024, 1, 15),
                    Remark = "Initial bulk order from UK",
                    SupplierId = new Guid("00000000-0000-0000-0000-000000000011"),  // Lipston Supplier
                    ProductId = new Guid("00000000-0000-0000-0000-000000000021"),   // Lipston Fresh Green Tea
                    CreatedAt = new DateTime(2024, 1, 15)
                },
                new SupplierOrder
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000032"),
                    SupplierOrderId = 2,
                    Quantity = 80,
                    OrderDate = new DateTime(2024, 2, 10),
                    Remark = "Quarterly restock",
                    SupplierId = new Guid("00000000-0000-0000-0000-000000000012"),  // Kingstone (India)
                    ProductId = new Guid("00000000-0000-0000-0000-000000000022"),   // TeaCoo Assam
                    CreatedAt = new DateTime(2024, 2, 10)
                },
                new SupplierOrder
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000033"),
                    SupplierOrderId = 3,
                    Quantity = 60,
                    OrderDate = new DateTime(2024, 3, 5),
                    Remark = null,
                    SupplierId = new Guid("00000000-0000-0000-0000-000000000013"),  // LaobanTeaHub
                    ProductId = new Guid("00000000-0000-0000-0000-000000000023"),   // Pu'erh
                    CreatedAt = new DateTime(2024, 3, 5)
                },
                new SupplierOrder
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000034"),
                    SupplierOrderId = 4,
                    Quantity = 40,
                    OrderDate = new DateTime(2024, 4, 18),
                    Remark = "Premium grade requested",
                    SupplierId = new Guid("00000000-0000-0000-0000-000000000014"),  // AloneTea
                    ProductId = new Guid("00000000-0000-0000-0000-000000000024"),   // NortLeaf Nuwera
                    CreatedAt = new DateTime(2024, 4, 18)
                },
                new SupplierOrder
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000035"),
                    SupplierOrderId = 5,
                    Quantity = 120,
                    OrderDate = new DateTime(2024, 5, 22),
                    Remark = "Organic certification verified",
                    SupplierId = new Guid("00000000-0000-0000-0000-000000000015"),  // TrolTrol
                    ProductId = new Guid("00000000-0000-0000-0000-000000000025"),   // HealthTea Mauna Kea
                    CreatedAt = new DateTime(2024, 5, 22)
                },
                new SupplierOrder
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000036"),
                    SupplierOrderId = 6,
                    Quantity = 200,
                    OrderDate = new DateTime(2024, 6, 30),
                    Remark = "Large order for European market",
                    SupplierId = new Guid("00000000-0000-0000-0000-000000000016"),  // HalTea
                    ProductId = new Guid("00000000-0000-0000-0000-000000000026"),   // HomeTea Irish
                    CreatedAt = new DateTime(2024, 6, 30)
                },
                new SupplierOrder
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000037"),
                    SupplierOrderId = 7,
                    Quantity = 70,
                    OrderDate = new DateTime(2024, 7, 12),
                    Remark = null,
                    SupplierId = new Guid("00000000-0000-0000-0000-000000000017"),  // Thia
                    ProductId = new Guid("00000000-0000-0000-0000-000000000027"),   // TeaFeast Earl Grey
                    CreatedAt = new DateTime(2024, 7, 12)
                },
                new SupplierOrder
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000038"),
                    SupplierOrderId = 8,
                    Quantity = 90,
                    OrderDate = new DateTime(2024, 8, 8),
                    Remark = "Express delivery requested",
                    SupplierId = new Guid("00000000-0000-0000-0000-000000000018"),  // Funck
                    ProductId = new Guid("00000000-0000-0000-0000-000000000028"),   // TeaForest Ostfries.
                    CreatedAt = new DateTime(2024, 8, 8)
                },
                new SupplierOrder
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000039"),
                    SupplierOrderId = 9,
                    Quantity = 110,
                    OrderDate = new DateTime(2024, 9, 17),
                    Remark = "Premium oolong batch",
                    SupplierId = new Guid("00000000-0000-0000-0000-000000000019"),  // OolongTeaHub
                    ProductId = new Guid("00000000-0000-0000-0000-000000000029"),   // SoftTeabank Oolong
                    CreatedAt = new DateTime(2024, 9, 17)
                },
                new SupplierOrder
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000040"),
                    SupplierOrderId = 10,
                    Quantity = 150,
                    OrderDate = new DateTime(2024, 10, 25),
                    Remark = "First shipment to Australia",
                    SupplierId = new Guid("00000000-0000-0000-0000-000000000020"),  // EdibleTea (Japan)
                    ProductId = new Guid("00000000-0000-0000-0000-000000000030"),   // TeaSeason Sencha
                    CreatedAt = new DateTime(2024, 10, 25)
                }
            );

        }
    }
}