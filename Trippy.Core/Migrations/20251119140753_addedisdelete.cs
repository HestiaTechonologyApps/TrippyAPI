using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trippy.Core.Migrations
{
    /// <inheritdoc />
    public partial class addedisdelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "VehicleMaintenanceRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TripOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TripNotes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "VehicleMaintenanceRecords");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TripOrders");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TripNotes");
        }
    }
}
