using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResturantManagmentSystemApp.Migrations
{
    /// <inheritdoc />
    public partial class FixTableColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TableNumber",
                table: "Tables",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tables",
                newName: "TableNumber");
        }
    }
}
