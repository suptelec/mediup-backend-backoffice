using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ADDSystemLigther : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SystemLigtherId",
                schema: "mup",
                table: "LigtherTransformers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SystemLigtherId",
                schema: "mup",
                table: "LigtherMetric",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SystemLigthers",
                schema: "mup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UrlAuthorizationDocument = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
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
                    table.PrimaryKey("PK_SystemLigthers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemLigthers_ElectriCompany_ElectricCompanyId",
                        column: x => x.ElectricCompanyId,
                        principalSchema: "mup",
                        principalTable: "ElectriCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LigtherTransformers_SystemLigtherId",
                schema: "mup",
                table: "LigtherTransformers",
                column: "SystemLigtherId");

            migrationBuilder.CreateIndex(
                name: "IX_LigtherMetric_SystemLigtherId",
                schema: "mup",
                table: "LigtherMetric",
                column: "SystemLigtherId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemLigthers_ElectricCompanyId",
                schema: "mup",
                table: "SystemLigthers",
                column: "ElectricCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_LigtherMetric_SystemLigthers_SystemLigtherId",
                schema: "mup",
                table: "LigtherMetric",
                column: "SystemLigtherId",
                principalSchema: "mup",
                principalTable: "SystemLigthers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LigtherTransformers_SystemLigthers_SystemLigtherId",
                schema: "mup",
                table: "LigtherTransformers",
                column: "SystemLigtherId",
                principalSchema: "mup",
                principalTable: "SystemLigthers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LigtherMetric_SystemLigthers_SystemLigtherId",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropForeignKey(
                name: "FK_LigtherTransformers_SystemLigthers_SystemLigtherId",
                schema: "mup",
                table: "LigtherTransformers");

            migrationBuilder.DropTable(
                name: "SystemLigthers",
                schema: "mup");

            migrationBuilder.DropIndex(
                name: "IX_LigtherTransformers_SystemLigtherId",
                schema: "mup",
                table: "LigtherTransformers");

            migrationBuilder.DropIndex(
                name: "IX_LigtherMetric_SystemLigtherId",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "SystemLigtherId",
                schema: "mup",
                table: "LigtherTransformers");

            migrationBuilder.DropColumn(
                name: "SystemLigtherId",
                schema: "mup",
                table: "LigtherMetric");
        }
    }
}
