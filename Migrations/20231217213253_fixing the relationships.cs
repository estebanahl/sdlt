using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backEnd.Migrations
{
    public partial class fixingtherelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_product_category_id",
                table: "product");

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "product_id",
                keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"));

            migrationBuilder.AlterColumn<string>(
                name: "image_url",
                table: "product",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "product_id",
                keyValue: new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "product_id",
                keyValue: new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                column: "image_url",
                value: "");

            migrationBuilder.CreateIndex(
                name: "IX_product_category_id",
                table: "product",
                column: "category_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_product_category_id",
                table: "product");

            migrationBuilder.AlterColumn<string>(
                name: "image_url",
                table: "product",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "product_id",
                keyValue: new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                column: "image_url",
                value: null);

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "product_id",
                keyValue: new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                column: "image_url",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_product_category_id",
                table: "product",
                column: "category_id",
                unique: true);
        }
    }
}
