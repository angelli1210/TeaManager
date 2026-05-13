using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeaManager.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Address", "BrandId", "BrandName", "BusinessRegNumber", "Country", "CreatedAt", "Email", "FoundedYear", "OwnerName", "Password", "Phone" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Lipston House, 42-43 Park Street, London, UK", 1, "Lipston", "BRN123445", "United Kingdom", new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@lipston.com", 1871, "Jorge Russell", "Lipton@123", "+44 5-1234-56344" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "TeaCoo Plaza, 343 MG Road, Mumbai, India", 2, "TeaCoo", "BRN883456", "India", new DateTime(2020, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@teacoo.com", 1950, "Priya Sundar", "Teacoo2345$", "+91 12-2234-56342" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "LaobanTea Firm, 57 Tea Street, Pu'er City, Yunnan Province, China", 3, "LaobanTea", "BRN323595", "China", new DateTime(2019, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@LaobanTea.com", 1855, "Li Wei", "LaoBanTea8473$", "+86 18-7533-34256" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "NortLeaf Plaza, 343 MG Road, Colombo, Sri Lanka", 4, "NortLeaf", "BRN833626", "Sri Lanka", new DateTime(2021, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@NortLeaf.com", 1950, "Suan Perera", "NortLeaf2334$", "+94 9384-32848" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "HealthTea Building, 123 Wellness Avenue, New York, USA", 5, "HealthTea", "BRN994567", "United States", new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@HealthTea.com", 2001, "Sarah Johnson", "HealthTea274@q", "+1 555-123-4567" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "HomeTea House, 456 Tea Lane, Dublin, Ireland", 6, "HomeTea", "BRN894567", "Ireland", new DateTime(2022, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@Hometea.com", 1900, "Sam Smith", "HomeTea3543$", "+353 1-234-434256" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "TeaFeast Plaza, 343 MG Road, Kuala Lumpur, Malaysia", 7, "TeaFeast", "BRN827456", "Malaysia", new DateTime(2021, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@Teafeast.com", 1950, "Ahmad Sandar", "Teafeast2345$", "+60 12-345-6789" },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "TeaForest Plaza, 343 MG Road, Nairobi, Kenya", 8, "TeaForest", "BRN883204", "Kenya", new DateTime(2023, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@TeaForest.com", 1950, "Samuel Harper", "Teaforest23749@", "+254 12-365-8890" },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "SoftTeabank Building, 123 Financial Avenue, Toronto, Canada", 9, "SoftTeabank", "BRN884446", "Canada", new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@softteabank.com", 1999, "Emily Davis", "SoftTeabank2345$", "+1 345-763-8901" },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "TeaSeason Building, 456 Tea Street, Sydney, Australia", 10, "TeaSeason", "BRN885557", "Australia", new DateTime(2023, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@TeaSeason.com", 1999, "Michael Johnson", "TeaSeason2345$", "+61 2-1234-5678" }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "ContactEmail", "Country", "CreatedAt", "Phone", "SupplierId", "SupplierName" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000011"), "supply@lipston.com", "United Kingdom", new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "+44 6-2346-83745", 1, "Lipston Supplier" },
                    { new Guid("00000000-0000-0000-0000-000000000012"), "supply@kingstone.com", "India", new DateTime(2020, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "+91 12-3334-55384", 2, "Kingstone Supplier" },
                    { new Guid("00000000-0000-0000-0000-000000000013"), "supply@LaobanTeaHub.com", "China", new DateTime(2019, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "+86 18-7532-34253", 3, "LaobanTeaHub Supplier" },
                    { new Guid("00000000-0000-0000-0000-000000000014"), "supply@alonetea.com", "Sri Lanka", new DateTime(2021, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "+94 90-8394-23456", 4, "AloneTea Supplier" },
                    { new Guid("00000000-0000-0000-0000-000000000015"), "supply@TrolTrol.com", "Sri Lanka", new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "+94 11-234-5678", 5, "TrolTrol Supplier" },
                    { new Guid("00000000-0000-0000-0000-000000000016"), "supply@HalTea.com", "Kenya", new DateTime(2022, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "+254 11-234-5678", 6, "HalTea Supplier" },
                    { new Guid("00000000-0000-0000-0000-000000000017"), "supply@Thia.com", "Thailand", new DateTime(2021, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "+66 2-123-4567", 7, "Thia Supplier" },
                    { new Guid("00000000-0000-0000-0000-000000000018"), "supply@Funck.com", "Germany", new DateTime(2023, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "+49 30-4568-2345", 8, "Funck Supplier" },
                    { new Guid("00000000-0000-0000-0000-000000000019"), "supply@OolongTeaHub.com", "China", new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "+86 10-1234-5678", 9, "OolongTeaHub Supplier" },
                    { new Guid("00000000-0000-0000-0000-000000000020"), "supply@EdibleTea.com", "Japan", new DateTime(2023, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "+81 3-1234-5678", 10, "EdibleTea Supplier" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CreatedAt", "Description", "HarvestYear", "Origin", "Price", "ProductId", "ProductName", "Stock", "SupplierId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000021"), new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2022, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "A refreshing green tea with a crisp flavor and various health benefits.", 2022, "Cornwell,United Kingdom", 18.59m, 1, "Lipston Fresh Green Tea", 80, new Guid("00000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-0000-0000-0000-000000000022"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2020, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "A flavorful black tea from the Assam region of India.", 2020, "Assam,India", 20.88m, 2, "TeaCoo Assam Black Tea", 80, new Guid("00000000-0000-0000-0000-000000000012") },
                    { new Guid("00000000-0000-0000-0000-000000000023"), new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2019, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "A rich and earthy tea from the Yunnan province of China, famous for its unique fermentation process.", 2019, "Yunnan,China", 30.24m, 3, "LaobanTea Pu'erh Tea", 20, new Guid("00000000-0000-0000-0000-000000000013") },
                    { new Guid("00000000-0000-0000-0000-000000000024"), new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2021, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "A flavorful black tea from the Nuwera Eliya region of Sri Lanka.", 2021, "Nuwera Eliya,Sri Lanka", 50.00m, 4, "NortLeaf Nuwera Premium Black Tea", 20, new Guid("00000000-0000-0000-0000-000000000014") },
                    { new Guid("00000000-0000-0000-0000-000000000025"), new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "An organic tea blend from the Mauna Kea region of Hawaii, beautifully balanced with floral and earthy notes.", 2022, "Hawaii,United States", 25.45m, 5, "HealthTea Organic Mauna Kea Tea", 80, new Guid("00000000-0000-0000-0000-000000000015") },
                    { new Guid("00000000-0000-0000-0000-000000000026"), new Guid("00000000-0000-0000-0000-000000000006"), new DateTime(2022, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fresh morning tea with a clear and flavorful taste, perfect for starting your day.", 2022, "Dublin,Ireland", 19.59m, 6, "HomeTea Irish Breakfast Tea", 150, new Guid("00000000-0000-0000-0000-000000000016") },
                    { new Guid("00000000-0000-0000-0000-000000000027"), new Guid("00000000-0000-0000-0000-000000000007"), new DateTime(2021, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Premium earl grey tea with a refreshing citrus flavor and a hint of bergamot, perfect for tea lovers.", 2021, "Chiang Mai,Thailand", 15.59m, 7, "TeaFeast Earl Grey Tea", 95, new Guid("00000000-0000-0000-0000-000000000017") },
                    { new Guid("00000000-0000-0000-0000-000000000028"), new Guid("00000000-0000-0000-0000-000000000008"), new DateTime(2023, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Blend of fabulous teas from Ostfriesland, Germany.", 2023, "Ostfriesland,Germany", 18.95m, 8, "TeaForest Ostfriesentee", 80, new Guid("00000000-0000-0000-0000-000000000018") },
                    { new Guid("00000000-0000-0000-0000-000000000029"), new Guid("00000000-0000-0000-0000-000000000009"), new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "High-quality oolong tea from Yunnan, China.", 2024, "Yunnan,China", 28.66m, 9, "SoftTeabank Premium Yunnan Oolong Tea", 95, new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-000000000030"), new Guid("00000000-0000-0000-0000-000000000010"), new DateTime(2023, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fantastic sencha green tea from Japan, with a fresh and grassy flavor.", 2024, "Shizuoka,Japan", 33.59m, 10, "TeaSeason Sencha Green Tea", 80, new Guid("00000000-0000-0000-0000-000000000020") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"));
        }
    }
}
