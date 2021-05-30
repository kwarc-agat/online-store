using Microsoft.EntityFrameworkCore.Migrations;

namespace lab12_1.Migrations
{
    public partial class Amends1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Category",
                table: "ArticleCreateViewModel",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "AGD" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "RTV" });

            migrationBuilder.InsertData(
                table: "Article",
                columns: new[] { "Id", "CategoryId", "Name", "Photo", "Price" },
                values: new object[] { 1, 1, "odkurzacz", "odkuracz.jpg", 599.99000000000001 });

            migrationBuilder.InsertData(
                table: "Article",
                columns: new[] { "Id", "CategoryId", "Name", "Photo", "Price" },
                values: new object[] { 2, 2, "radio", "radio.jpg", 79.950000000000003 });
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

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "ArticleCreateViewModel",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
