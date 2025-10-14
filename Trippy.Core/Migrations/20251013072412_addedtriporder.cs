using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trippy.Core.Migrations
{
    /// <inheritdoc />
    public partial class addedtriporder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TripBookingModes",
                columns: table => new
                {
                    TripBookingModeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripBookingModeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripBookingModes", x => x.TripBookingModeId);
                });

            migrationBuilder.CreateTable(
                name: "TripNotes",
                columns: table => new
                {
                    TripNoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripNote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripNotes", x => x.TripNoteId);
                });

            migrationBuilder.CreateTable(
                name: "TripOrders",
                columns: table => new
                {
                    TripOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripBookingModeId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FromLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToLocation1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToLocation2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToLocation3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToLocation4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TripDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TripStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TripAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdvanceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BalanceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedUser = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripOrders", x => x.TripOrderId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripBookingModes");

            migrationBuilder.DropTable(
                name: "TripNotes");

            migrationBuilder.DropTable(
                name: "TripOrders");
        }
    }
}
