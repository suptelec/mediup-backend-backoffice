using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityEnergyMeasurementSummary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Year",
                schema: "mup",
                table: "EnergyMeasurementSummaries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                schema: "mup",
                table: "EnergyMeasurementSummaries");
        }
    }
}
