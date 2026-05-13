using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeaManager.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedSupplierOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SupplierOrders",
                columns: new[] { "Id", "CreatedAt", "OrderDate", "ProductId", "Quantity", "Remark", "SupplierId", "SupplierOrderId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000031"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000021"), 100, "Initial bulk order from UK", new Guid("00000000-0000-0000-0000-000000000011"), 1 },
                    { new Guid("00000000-0000-0000-0000-000000000032"), new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000022"), 80, "Quarterly restock", new Guid("00000000-0000-0000-0000-000000000012"), 2 },
                    { new Guid("00000000-0000-0000-0000-000000000033"), new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000023"), 60, null, new Guid("00000000-0000-0000-0000-000000000013"), 3 },
                    { new Guid("00000000-0000-0000-0000-000000000034"), new DateTime(2024, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000024"), 40, "Premium grade requested", new Guid("00000000-0000-0000-0000-000000000014"), 4 },
                    { new Guid("00000000-0000-0000-0000-000000000035"), new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000025"), 120, "Organic certification verified", new Guid("00000000-0000-0000-0000-000000000015"), 5 },
                    { new Guid("00000000-0000-0000-0000-000000000036"), new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000026"), 200, "Large order for European market", new Guid("00000000-0000-0000-0000-000000000016"), 6 },
                    { new Guid("00000000-0000-0000-0000-000000000037"), new DateTime(2024, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000027"), 70, null, new Guid("00000000-0000-0000-0000-000000000017"), 7 },
                    { new Guid("00000000-0000-0000-0000-000000000038"), new DateTime(2024, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000028"), 90, "Express delivery requested", new Guid("00000000-0000-0000-0000-000000000018"), 8 },
                    { new Guid("00000000-0000-0000-0000-000000000039"), new DateTime(2024, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000029"), 110, "Premium oolong batch", new Guid("00000000-0000-0000-0000-000000000019"), 9 },
                    { new Guid("00000000-0000-0000-0000-000000000040"), new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000030"), 150, "First shipment to Australia", new Guid("00000000-0000-0000-0000-000000000020"), 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SupplierOrders",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"));

            migrationBuilder.DeleteData(
                table: "SupplierOrders",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"));

            migrationBuilder.DeleteData(
                table: "SupplierOrders",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"));

            migrationBuilder.DeleteData(
                table: "SupplierOrders",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"));

            migrationBuilder.DeleteData(
                table: "SupplierOrders",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"));

            migrationBuilder.DeleteData(
                table: "SupplierOrders",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"));

            migrationBuilder.DeleteData(
                table: "SupplierOrders",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"));

            migrationBuilder.DeleteData(
                table: "SupplierOrders",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"));

            migrationBuilder.DeleteData(
                table: "SupplierOrders",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"));

            migrationBuilder.DeleteData(
                table: "SupplierOrders",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"));
        }
    }
}
