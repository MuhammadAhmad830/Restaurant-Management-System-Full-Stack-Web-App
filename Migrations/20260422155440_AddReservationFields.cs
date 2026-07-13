using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResturantManagmentSystemApp.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReserved",
                table: "Tables",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReservationTime",
                table: "Tables",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReservedFor",
                table: "Tables",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReserved",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "ReservationTime",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "ReservedFor",
                table: "Tables");
        }
    }
}
