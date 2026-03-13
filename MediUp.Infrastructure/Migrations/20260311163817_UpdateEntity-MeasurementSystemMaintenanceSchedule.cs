using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityMeasurementSystemMaintenanceSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ScheduledDate",
                schema: "mup",
                table: "MeasurementSystemMaintenanceSchedules",
                newName: "PeriodTo");

            migrationBuilder.AddColumn<DateTime>(
                name: "PeriodFrom",
                schema: "mup",
                table: "MeasurementSystemMaintenanceSchedules",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Spreadsheet",
                schema: "mup",
                table: "MeasurementSystemMaintenanceSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                schema: "mup",
                table: "MeasurementSystemMaintenanceSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeriodFrom",
                schema: "mup",
                table: "MeasurementSystemMaintenanceSchedules");

            migrationBuilder.DropColumn(
                name: "Spreadsheet",
                schema: "mup",
                table: "MeasurementSystemMaintenanceSchedules");

            migrationBuilder.DropColumn(
                name: "Year",
                schema: "mup",
                table: "MeasurementSystemMaintenanceSchedules");

            migrationBuilder.RenameColumn(
                name: "PeriodTo",
                schema: "mup",
                table: "MeasurementSystemMaintenanceSchedules",
                newName: "ScheduledDate");
        }
    }
}
