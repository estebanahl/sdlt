using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backEnd.Migrations
{
    public partial class Freshstart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "event",
                columns: table => new
                {
                    event_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event", x => x.event_id);
                });

            
            migrationBuilder.CreateTable(
                name: "booking",
                columns: table => new
                {
                    booking_id = table.Column<Guid>(type: "uuid", nullable: false),
                    datetime = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 60, nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking", x => x.booking_id);
                    table.ForeignKey(
                        name: "FK_booking_AspNetUsers_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

        

            migrationBuilder.CreateTable(
                name: "event_booking",
                columns: table => new
                {
                    event_id = table.Column<Guid>(type: "uuid", nullable: false),
                    booking_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_booking", x => new { x.event_id, x.booking_id });
                    table.ForeignKey(
                        name: "FK_event_booking_booking_booking_id",
                        column: x => x.booking_id,
                        principalTable: "booking",
                        principalColumn: "booking_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_event_booking_event_event_id",
                        column: x => x.event_id,
                        principalTable: "event",
                        principalColumn: "event_id",
                        onDelete: ReferentialAction.Cascade);
                });

           

            migrationBuilder.CreateIndex(
                name: "IX_booking_user_id",
                table: "booking",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_booking_booking_id",
                table: "event_booking",
                column: "booking_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
