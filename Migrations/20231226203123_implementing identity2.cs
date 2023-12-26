using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backEnd.Migrations
{
    public partial class implementingidentity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5c511ad1-794a-4a76-b9c1-fbe6d172e092", "e0056c4a-23f8-4fbb-8f9e-2a843c1a3861", "Manager", "MANAGER" },
                    { "6c2c9cb8-599e-4795-ab1a-cd6caa52f79d", "9ee88c93-c31d-4c8b-b1b1-490f2a56da06", "Administrator", "ADMINISTRATOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c511ad1-794a-4a76-b9c1-fbe6d172e092");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c2c9cb8-599e-4795-ab1a-cd6caa52f79d");
        }
    }
}
