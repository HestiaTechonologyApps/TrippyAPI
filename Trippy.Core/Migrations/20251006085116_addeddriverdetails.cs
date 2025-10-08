using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trippy.CORE.Migrations
{
    /// <inheritdoc />
    public partial class addeddriverdetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppNotifications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppNotifications",
                columns: table => new
                {
                    AppNotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    NotificationImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotificationLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotificationTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotificationType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppNotifications", x => x.AppNotificationId);
                });
        }
    }
}
