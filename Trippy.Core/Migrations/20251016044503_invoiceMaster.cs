using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trippy.Core.Migrations
{
    /// <inheritdoc />
    public partial class invoiceMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseOrderId",
                table: "InvoiceMasters");

            migrationBuilder.AddColumn<int>(
                name: "TripOrderId",
                table: "InvoiceDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TripOrderId",
                table: "InvoiceDetails");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseOrderId",
                table: "InvoiceMasters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
