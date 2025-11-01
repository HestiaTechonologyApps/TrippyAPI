using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trippy.Core.Migrations
{
    /// <inheritdoc />
    public partial class addeddb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_vehicleMaintenanceRecords",
                table: "vehicleMaintenanceRecords");

            migrationBuilder.RenameTable(
                name: "vehicleMaintenanceRecords",
                newName: "VehicleMaintenanceRecords");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleMaintenanceRecords",
                table: "VehicleMaintenanceRecords",
                column: "VehicleMaintenanceRecordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleMaintenanceRecords",
                table: "VehicleMaintenanceRecords");

            migrationBuilder.RenameTable(
                name: "VehicleMaintenanceRecords",
                newName: "vehicleMaintenanceRecords");

            migrationBuilder.AddPrimaryKey(
                name: "PK_vehicleMaintenanceRecords",
                table: "vehicleMaintenanceRecords",
                column: "VehicleMaintenanceRecordId");
        }
    }
}
