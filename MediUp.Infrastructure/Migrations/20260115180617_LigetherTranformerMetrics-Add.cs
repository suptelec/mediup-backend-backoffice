using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LigetherTranformerMetricsAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Brand",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Class",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LigtherTransformers",
                schema: "mup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Serial = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Codigo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Model = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Brand = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Class = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastCalibrationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    NextCalibrationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UrlPicture = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    PrimaryCurrent = table.Column<double>(type: "double", nullable: false),
                    SecondaryCurrent = table.Column<double>(type: "double", nullable: false),
                    PrimaryVoltage = table.Column<double>(type: "double", nullable: false),
                    SecondaryVoltage = table.Column<double>(type: "double", nullable: false),
                    ElectricCompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LigtherTransformers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LigtherTransformerMetrics",
                schema: "mup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LigtherTransformerId = table.Column<long>(type: "bigint", nullable: false),
                    LigtherMetricId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LigtherTransformerMetrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LigtherTransformerMetrics_LigtherMetric_LigtherMetricId",
                        column: x => x.LigtherMetricId,
                        principalSchema: "mup",
                        principalTable: "LigtherMetric",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LigtherTransformerMetrics_LigtherTransformers_LigtherTransfo~",
                        column: x => x.LigtherTransformerId,
                        principalSchema: "mup",
                        principalTable: "LigtherTransformers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LigtherTransformerMetrics_LigtherMetricId",
                schema: "mup",
                table: "LigtherTransformerMetrics",
                column: "LigtherMetricId");

            migrationBuilder.CreateIndex(
                name: "IX_LigtherTransformerMetrics_LigtherTransformerId",
                schema: "mup",
                table: "LigtherTransformerMetrics",
                column: "LigtherTransformerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LigtherTransformerMetrics",
                schema: "mup");

            migrationBuilder.DropTable(
                name: "LigtherTransformers",
                schema: "mup");

            migrationBuilder.DropColumn(
                name: "Brand",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "Class",
                schema: "mup",
                table: "LigtherMetric");
        }
    }
}
