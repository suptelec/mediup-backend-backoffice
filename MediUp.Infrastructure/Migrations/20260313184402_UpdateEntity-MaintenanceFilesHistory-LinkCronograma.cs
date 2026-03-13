using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityMaintenanceFilesHistoryLinkCronograma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MonthNumber",
                schema: "mup",
                table: "MaintenanceFilesHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SystemLigtherName",
                schema: "mup",
                table: "MaintenanceFilesHistories",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "WorkOrderhNumber",
                schema: "mup",
                table: "MaintenanceFilesHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                schema: "mup",
                table: "MaintenanceFilesHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthNumber",
                schema: "mup",
                table: "MaintenanceFilesHistories");

            migrationBuilder.DropColumn(
                name: "SystemLigtherName",
                schema: "mup",
                table: "MaintenanceFilesHistories");

            migrationBuilder.DropColumn(
                name: "WorkOrderhNumber",
                schema: "mup",
                table: "MaintenanceFilesHistories");

            migrationBuilder.DropColumn(
                name: "Year",
                schema: "mup",
                table: "MaintenanceFilesHistories");
        }
    }
}
