using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trippy.Core.Migrations
{
    /// <inheritdoc />
    public partial class addedexpensse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExpenseTypeCode",
                table: "ExpenseTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExpenseVoucher",
                table: "ExpenseMasters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentMode",
                table: "ExpenseMasters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RelatedEntityId",
                table: "ExpenseMasters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RelatedEntityType",
                table: "ExpenseMasters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpenseTypeCode",
                table: "ExpenseTypes");

            migrationBuilder.DropColumn(
                name: "ExpenseVoucher",
                table: "ExpenseMasters");

            migrationBuilder.DropColumn(
                name: "PaymentMode",
                table: "ExpenseMasters");

            migrationBuilder.DropColumn(
                name: "RelatedEntityId",
                table: "ExpenseMasters");

            migrationBuilder.DropColumn(
                name: "RelatedEntityType",
                table: "ExpenseMasters");
        }
    }
}
