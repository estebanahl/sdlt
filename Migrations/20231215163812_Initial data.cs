using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backEnd.Migrations
{
    public partial class Initialdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "category",
                columns: new[] { "category_id", "description", "name" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "", "Carnes" },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "", "Ensaldas" }
                });

            migrationBuilder.InsertData(
                table: "product",
                columns: new[] { "product_id", "active", "category_id", "description", "image_url", "name" },
                values: new object[,]
                {
                    { new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"), true, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Pernil de cerdo fileteado curado por 2 años", null, "Pernil de cerdo fileteado" },
                    { new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), true, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Ensalada de repollo con vinagre abadía", null, "Ensalada de repollo" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "product_id",
                keyValue: new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"));

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "product_id",
                keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"));

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "product_id",
                keyValue: new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"));

            migrationBuilder.DeleteData(
                table: "category",
                keyColumn: "category_id",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"));

            migrationBuilder.DeleteData(
                table: "category",
                keyColumn: "category_id",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
        }
    }
}
