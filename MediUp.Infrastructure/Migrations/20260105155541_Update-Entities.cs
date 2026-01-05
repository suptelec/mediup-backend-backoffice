using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManufacturingYear",
                schema: "mup",
                table: "LigtherMetric",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Sector",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UrlCapture",
                schema: "mup",
                table: "EnergyMeasurementDownloads",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UrlZip",
                schema: "mup",
                table: "EnergyMeasurementDownloads",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManufacturingYear",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "Province",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "Sector",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "UrlCapture",
                schema: "mup",
                table: "EnergyMeasurementDownloads");

            migrationBuilder.DropColumn(
                name: "UrlZip",
                schema: "mup",
                table: "EnergyMeasurementDownloads");
        }
    }
}
