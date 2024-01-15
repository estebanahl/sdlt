using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backEnd.Migrations
{
    public partial class CategorytoProductCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_category_category_id",
                table: "product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_category",
                table: "category");

            migrationBuilder.RenameTable(
                name: "category",
                newName: "product_category");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product_category",
                table: "product_category",
                column: "category_id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_product_category_category_id",
                table: "product",
                column: "category_id",
                principalTable: "product_category",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_product_category_category_id",
                table: "product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product_category",
                table: "product_category");

            migrationBuilder.RenameTable(
                name: "product_category",
                newName: "category");

            migrationBuilder.AddPrimaryKey(
                name: "PK_category",
                table: "category",
                column: "category_id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_category_category_id",
                table: "product",
                column: "category_id",
                principalTable: "category",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
