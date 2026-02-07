using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertiesLigtherMetric : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BatteryLevelPercent",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Gateway",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "MacAddress",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SubnetMask",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SwitchLanPort",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TcRatio",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TpRatio",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatteryLevelPercent",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "Gateway",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "MacAddress",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "SubnetMask",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "SwitchLanPort",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "TcRatio",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "TpRatio",
                schema: "mup",
                table: "LigtherMetric");
        }
    }
}
