using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResturantManagmentSystemApp.Migrations
{
    public partial class FixOrderDateProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Purana column khatam karein
            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Orders");

            // 2. Status column ki type badalne ke liye manual SQL (PostgreSQL Fix)
            migrationBuilder.Sql("ALTER TABLE \"Orders\" ALTER COLUMN \"Status\" TYPE integer USING (CASE WHEN \"Status\" = '' OR \"Status\" IS NULL THEN 0 ELSE \"Status\"::integer END);");

            // 3. Naya CreatedAt column add karein
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: DateTime.UtcNow);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}