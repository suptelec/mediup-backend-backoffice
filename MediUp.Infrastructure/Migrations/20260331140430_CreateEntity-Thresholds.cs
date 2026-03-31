using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MediUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateEntityThresholds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PowerFactorThresholds",
                schema: "mup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Level = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MinValue = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    MaxValue = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
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
                    table.PrimaryKey("PK_PowerFactorThresholds", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VoltageThresholds",
                schema: "mup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NominalKv = table.Column<decimal>(type: "decimal(5,1)", nullable: false),
                    LowerNormal = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    LowerEmergent = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    UpperNormal = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    UpperEmergent = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoltageThresholds", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                schema: "mup",
                table: "PowerFactorThresholds",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "Level", "MaxValue", "MinValue", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "Factor de potencia crítico", "Critical", 0.60m, null, null, null },
                    { 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "Bajo - Penalizado", "Low", 0.94m, 0.61m, null, null },
                    { 3L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "Normal", "Normal", 1.00m, 0.95m, null, null }
                });

            migrationBuilder.InsertData(
                schema: "mup",
                table: "VoltageThresholds",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "LowerEmergent", "LowerNormal", "NominalKv", "UpdatedAt", "UpdatedBy", "UpperEmergent", "UpperNormal" },
                values: new object[,]
                {
                    { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", -0.06m, -0.05m, 138m, null, null, 0.06m, 0.05m },
                    { 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", -0.05m, -0.03m, 22m, null, null, 0.06m, 0.04m },
                    { 3L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 0.00m, -0.03m, 6.6m, null, null, 0.00m, 0.04m },
                    { 4L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", -0.06m, -0.05m, 480m, null, null, 0.06m, 0.05m },
                    { 5L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", -0.06m, -0.05m, 220m, null, null, 0.06m, 0.05m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PowerFactorThresholds",
                schema: "mup");

            migrationBuilder.DropTable(
                name: "VoltageThresholds",
                schema: "mup");
        }
    }
}
