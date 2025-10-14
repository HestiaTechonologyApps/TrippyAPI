using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trippy.Core.Migrations
{
    /// <inheritdoc />
    public partial class addedcustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TripOrders");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "TripOrders");

            migrationBuilder.RenameColumn(
                name: "LastModifiedUser",
                table: "TripOrders",
                newName: "ToDateString");

            migrationBuilder.RenameColumn(
                name: "CreatedByUser",
                table: "TripOrders",
                newName: "FromDateString");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ToDateString",
                table: "TripOrders",
                newName: "LastModifiedUser");

            migrationBuilder.RenameColumn(
                name: "FromDateString",
                table: "TripOrders",
                newName: "CreatedByUser");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TripOrders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "TripOrders",
                type: "datetime2",
                nullable: true);
        }
    }
}
