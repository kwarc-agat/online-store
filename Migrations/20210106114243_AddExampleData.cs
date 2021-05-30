using Microsoft.EntityFrameworkCore.Migrations;

namespace lab12_1.Migrations
{
    public partial class AddExampleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Article",
                columns: new[] { "Id", "Category", "Name", "Photo", "Price" },
                values: new object[,]
                {
                    { 1, "AGD", "odkurzacz", "odkuracz.jpg", 599.99000000000001 },
                    { 2, "RTV", "radio", "radio.jpg", 79.950000000000003 }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "AGD" },
                    { 2, "RTV" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
