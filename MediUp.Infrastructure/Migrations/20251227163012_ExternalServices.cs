using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExternalServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnergyMeasurementDownloads",
                schema: "mup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Meter = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MeasurementDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IntegrationStatus = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyMeasurementDownloads", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EnergyMeasurementData",
                schema: "mup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ExternalId = table.Column<int>(type: "int", nullable: false),
                    MeasuredAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MeasuredAtIso = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    QuarterHour = table.Column<int>(type: "int", nullable: false),
                    ActiveEnergyDeliveredKwh = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ActiveEnergyReceivedKwh = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ReactiveEnergyDeliveredKvarh = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ReactiveEnergyReceivedKvarh = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ApparentEnergyDeliveredKvah = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    IntegrationPeriodSeconds = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    AverageVoltageKv = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Frequency = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    EnergyMeasurementDownloadId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyMeasurementData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnergyMeasurementData_EnergyMeasurementDownloads_EnergyMeasu~",
                        column: x => x.EnergyMeasurementDownloadId,
                        principalSchema: "mup",
                        principalTable: "EnergyMeasurementDownloads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EnergyMeasurementEvents",
                schema: "mup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OccurredAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EnergyMeasurementDownloadId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyMeasurementEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnergyMeasurementEvents_EnergyMeasurementDownloads_EnergyMea~",
                        column: x => x.EnergyMeasurementDownloadId,
                        principalSchema: "mup",
                        principalTable: "EnergyMeasurementDownloads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EnergyMeasurementData_EnergyMeasurementDownloadId",
                schema: "mup",
                table: "EnergyMeasurementData",
                column: "EnergyMeasurementDownloadId");

            migrationBuilder.CreateIndex(
                name: "IX_EnergyMeasurementEvents_EnergyMeasurementDownloadId",
                schema: "mup",
                table: "EnergyMeasurementEvents",
                column: "EnergyMeasurementDownloadId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnergyMeasurementData",
                schema: "mup");

            migrationBuilder.DropTable(
                name: "EnergyMeasurementEvents",
                schema: "mup");

            migrationBuilder.DropTable(
                name: "EnergyMeasurementDownloads",
                schema: "mup");
        }
    }
}
