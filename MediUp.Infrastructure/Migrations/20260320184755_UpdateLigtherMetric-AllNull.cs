using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLigtherMetricAllNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TpSecondaryRatio",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TerminalBlockSealTwo",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TerminalBlockSealOne",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TcSecondaryRatio",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "PartNumber",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ModelNumber",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "MainMeterSeal",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ActiveRouter",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ActiveMeter",
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
                keyColumn: "TpSecondaryRatio",
                keyValue: null,
                column: "TpSecondaryRatio",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TpSecondaryRatio",
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
                keyColumn: "TerminalBlockSealTwo",
                keyValue: null,
                column: "TerminalBlockSealTwo",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TerminalBlockSealTwo",
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
                keyColumn: "TerminalBlockSealOne",
                keyValue: null,
                column: "TerminalBlockSealOne",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TerminalBlockSealOne",
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
                keyColumn: "TcSecondaryRatio",
                keyValue: null,
                column: "TcSecondaryRatio",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TcSecondaryRatio",
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
                keyColumn: "PartNumber",
                keyValue: null,
                column: "PartNumber",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "PartNumber",
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
                keyColumn: "ModelNumber",
                keyValue: null,
                column: "ModelNumber",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ModelNumber",
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
                keyColumn: "MainMeterSeal",
                keyValue: null,
                column: "MainMeterSeal",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "MainMeterSeal",
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
                keyColumn: "ActiveRouter",
                keyValue: null,
                column: "ActiveRouter",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ActiveRouter",
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
                keyColumn: "ActiveMeter",
                keyValue: null,
                column: "ActiveMeter",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ActiveMeter",
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
