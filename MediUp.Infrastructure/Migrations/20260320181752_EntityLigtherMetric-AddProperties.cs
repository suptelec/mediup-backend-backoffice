using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntityLigtherMetricAddProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActiveMeter",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ActiveRouter",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "MainMeterSeal",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ModelNumber",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PartNumber",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TcSecondaryRatio",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TerminalBlockSealOne",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TerminalBlockSealTwo",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TpSecondaryRatio",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveMeter",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "ActiveRouter",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "MainMeterSeal",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "ModelNumber",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "PartNumber",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "TcSecondaryRatio",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "TerminalBlockSealOne",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "TerminalBlockSealTwo",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "TpSecondaryRatio",
                schema: "mup",
                table: "LigtherMetric");
        }
    }
}
