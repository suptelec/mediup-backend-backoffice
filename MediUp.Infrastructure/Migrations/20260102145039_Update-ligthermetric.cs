using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updateligthermetric : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Altitude",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "InstallationDate",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "Permission",
                schema: "mup",
                table: "Agents");

            migrationBuilder.RenameColumn(
                name: "LastMaintenanceAt",
                schema: "mup",
                table: "LigtherMetric",
                newName: "NextCalibrationDate");

            migrationBuilder.RenameColumn(
                name: "FirmwareVersion",
                schema: "mup",
                table: "LigtherMetric",
                newName: "Version");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                schema: "mup",
                table: "LigtherMetric",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                schema: "mup",
                table: "LigtherMetric",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastCalibrationDate",
                schema: "mup",
                table: "LigtherMetric",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Codigo",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "LastCalibrationDate",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.RenameColumn(
                name: "Version",
                schema: "mup",
                table: "LigtherMetric",
                newName: "FirmwareVersion");

            migrationBuilder.RenameColumn(
                name: "NextCalibrationDate",
                schema: "mup",
                table: "LigtherMetric",
                newName: "LastMaintenanceAt");

            migrationBuilder.UpdateData(
                schema: "mup",
                table: "LigtherMetric",
                keyColumn: "Status",
                keyValue: null,
                column: "Status",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                schema: "mup",
                table: "LigtherMetric",
                type: "decimal(65,30)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                schema: "mup",
                table: "LigtherMetric",
                type: "decimal(65,30)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AddColumn<decimal>(
                name: "Altitude",
                schema: "mup",
                table: "LigtherMetric",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "InstallationDate",
                schema: "mup",
                table: "LigtherMetric",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Permission",
                schema: "mup",
                table: "Agents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
