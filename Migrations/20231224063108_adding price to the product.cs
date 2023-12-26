using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backEnd.Migrations
{
    public partial class addingpricetotheproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "product",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "product",
                columns: new[] { "product_id", "active", "category_id", "description", "image_url", "name", "price" },
                values: new object[] { new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), true, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Ensalada rusa con mayonesa natura, zanahoria en cubos y arvejas", "", "Ensalada Rusa", 0m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "product_id",
                keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"));

            migrationBuilder.DropColumn(
                name: "price",
                table: "product");
        }
    }
}
