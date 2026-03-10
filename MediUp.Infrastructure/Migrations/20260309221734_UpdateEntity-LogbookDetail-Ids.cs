using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityLogbookDetailIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SystemLigtherId",
                schema: "mup",
                table: "Logbooks");

            migrationBuilder.AddColumn<long>(
                name: "LigtherMetricId",
                schema: "mup",
                table: "LogbookDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SystemLigtherId",
                schema: "mup",
                table: "LogbookDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LigtherMetricId",
                schema: "mup",
                table: "LogbookDetails");

            migrationBuilder.DropColumn(
                name: "SystemLigtherId",
                schema: "mup",
                table: "LogbookDetails");

            migrationBuilder.AddColumn<long>(
                name: "SystemLigtherId",
                schema: "mup",
                table: "Logbooks",
                type: "bigint",
                nullable: true);
        }
    }
}
