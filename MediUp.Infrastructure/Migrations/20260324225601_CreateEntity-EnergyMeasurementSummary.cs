using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateEntityEnergyMeasurementSummary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnergyMeasurementSummaries",
                schema: "mup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdEmpresa = table.Column<long>(type: "bigint", nullable: false),
                    IdSystemaMedida = table.Column<long>(type: "bigint", nullable: false),
                    Codigo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TPlCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Month = table.Column<int>(type: "int", nullable: false),
                    TotalActiveEnergyDeliveredKwh = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TotalActiveEnergyReceivedKwh = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TotalReactiveEnergyDeliveredKvarh = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TotalReactiveEnergyReceivedKvarh = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TotalApparentEnergyDeliveredKvah = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyMeasurementSummaries", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnergyMeasurementSummaries",
                schema: "mup");
        }
    }
}
