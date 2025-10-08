using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trippy.CORE.Migrations
{
    /// <inheritdoc />
    public partial class addednewfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Deivername",
                table: "Drivers",
                newName: "DriverName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DriverName",
                table: "Drivers",
                newName: "Deivername");
        }
    }
}
