using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventoryService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedInventoryData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "InventoryItems",
                columns: new[] { "Id", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { "01JGNX5AA0QQNVENZZRY000001", "01JGNX4SP0QZVS6QF4R3ZCWGA1", 50 },
                    { "01JGNX5AA0QQNVENZZRY000002", "01JGNX4SP0QZVS6QF4R3ZCWGA2", 100 },
                    { "01JGNX5AA0QQNVENZZRY000003", "01JGNX4SP0QZVS6QF4R3ZCWGA3", 200 },
                    { "01JGNX5AA0QQNVENZZRY000004", "01JGNX4SP0QZVS6QF4R3ZCWGA4", 150 },
                    { "01JGNX5AA0QQNVENZZRY000005", "01JGNX4SP0QZVS6QF4R3ZCWGA5", 30 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: "01JGNX5AA0QQNVENZZRY000001");

            migrationBuilder.DeleteData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: "01JGNX5AA0QQNVENZZRY000002");

            migrationBuilder.DeleteData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: "01JGNX5AA0QQNVENZZRY000003");

            migrationBuilder.DeleteData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: "01JGNX5AA0QQNVENZZRY000004");

            migrationBuilder.DeleteData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: "01JGNX5AA0QQNVENZZRY000005");
        }
    }
}
