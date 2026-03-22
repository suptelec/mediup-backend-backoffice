using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLigtherMetricNulleableValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Sector",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "NetworkType",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CENACECode",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "mup",
                table: "LigtherMetric",
                keyColumn: "Sector",
                keyValue: null,
                column: "Sector",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Sector",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                schema: "mup",
                table: "LigtherMetric",
                keyColumn: "NetworkType",
                keyValue: null,
                column: "NetworkType",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "NetworkType",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                schema: "mup",
                table: "LigtherMetric",
                keyColumn: "CENACECode",
                keyValue: null,
                column: "CENACECode",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CENACECode",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
