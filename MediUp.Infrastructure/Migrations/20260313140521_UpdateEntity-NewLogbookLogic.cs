using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityNewLogbookLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogbookDetails_Logbooks_LogbookId",
                schema: "mup",
                table: "LogbookDetails");

            migrationBuilder.DropTable(
                name: "Logbooks",
                schema: "mup");

            migrationBuilder.RenameColumn(
                name: "LogbookId",
                schema: "mup",
                table: "LogbookDetails",
                newName: "ElectricCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_LogbookDetails_LogbookId",
                schema: "mup",
                table: "LogbookDetails",
                newName: "IX_LogbookDetails_ElectricCompanyId");

            migrationBuilder.AddColumn<int>(
                name: "MonthNumber",
                schema: "mup",
                table: "LogbookDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportNumber",
                schema: "mup",
                table: "LogbookDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_LogbookDetails_ElectriCompany_ElectricCompanyId",
                schema: "mup",
                table: "LogbookDetails",
                column: "ElectricCompanyId",
                principalSchema: "mup",
                principalTable: "ElectriCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogbookDetails_ElectriCompany_ElectricCompanyId",
                schema: "mup",
                table: "LogbookDetails");

            migrationBuilder.DropColumn(
                name: "MonthNumber",
                schema: "mup",
                table: "LogbookDetails");

            migrationBuilder.DropColumn(
                name: "ReportNumber",
                schema: "mup",
                table: "LogbookDetails");

            migrationBuilder.RenameColumn(
                name: "ElectricCompanyId",
                schema: "mup",
                table: "LogbookDetails",
                newName: "LogbookId");

            migrationBuilder.RenameIndex(
                name: "IX_LogbookDetails_ElectricCompanyId",
                schema: "mup",
                table: "LogbookDetails",
                newName: "IX_LogbookDetails_LogbookId");

            migrationBuilder.CreateTable(
                name: "Logbooks",
                schema: "mup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ElectricCompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MonthNumber = table.Column<int>(type: "int", nullable: false),
                    PeriodFrom = table.Column<DateOnly>(type: "date", nullable: false),
                    PeriodTo = table.Column<DateOnly>(type: "date", nullable: false),
                    ReportNumber = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logbooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logbooks_ElectriCompany_ElectricCompanyId",
                        column: x => x.ElectricCompanyId,
                        principalSchema: "mup",
                        principalTable: "ElectriCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Logbooks_ElectricCompanyId",
                schema: "mup",
                table: "Logbooks",
                column: "ElectricCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_LogbookDetails_Logbooks_LogbookId",
                schema: "mup",
                table: "LogbookDetails",
                column: "LogbookId",
                principalSchema: "mup",
                principalTable: "Logbooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
