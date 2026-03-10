using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityLogbook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ElectricCompanyId",
                schema: "mup",
                table: "Logbooks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SystemLigtherId",
                schema: "mup",
                table: "Logbooks",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Logbooks_ElectricCompanyId",
                schema: "mup",
                table: "Logbooks",
                column: "ElectricCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logbooks_ElectriCompany_ElectricCompanyId",
                schema: "mup",
                table: "Logbooks",
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
                name: "FK_Logbooks_ElectriCompany_ElectricCompanyId",
                schema: "mup",
                table: "Logbooks");

            migrationBuilder.DropIndex(
                name: "IX_Logbooks_ElectricCompanyId",
                schema: "mup",
                table: "Logbooks");

            migrationBuilder.DropColumn(
                name: "ElectricCompanyId",
                schema: "mup",
                table: "Logbooks");

            migrationBuilder.DropColumn(
                name: "SystemLigtherId",
                schema: "mup",
                table: "Logbooks");
        }
    }
}
