using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trippy.Core.Migrations
{
    /// <inheritdoc />
    public partial class addedTakeoftime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "VehicleTakeOfTime",
                table: "TripOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "InvoiceDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDiscount",
                table: "InvoiceDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetailTaxes_InvoiceDetailId",
                table: "InvoiceDetailTaxes",
                column: "InvoiceDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_InvoicemasterId",
                table: "InvoiceDetails",
                column: "InvoicemasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_InvoiceMasters_InvoicemasterId",
                table: "InvoiceDetails",
                column: "InvoicemasterId",
                principalTable: "InvoiceMasters",
                principalColumn: "InvoicemasterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetailTaxes_InvoiceDetails_InvoiceDetailId",
                table: "InvoiceDetailTaxes",
                column: "InvoiceDetailId",
                principalTable: "InvoiceDetails",
                principalColumn: "InvoiceDetailId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_InvoiceMasters_InvoicemasterId",
                table: "InvoiceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetailTaxes_InvoiceDetails_InvoiceDetailId",
                table: "InvoiceDetailTaxes");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceDetailTaxes_InvoiceDetailId",
                table: "InvoiceDetailTaxes");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceDetails_InvoicemasterId",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "VehicleTakeOfTime",
                table: "TripOrders");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "TotalDiscount",
                table: "InvoiceDetails");
        }
    }
}
