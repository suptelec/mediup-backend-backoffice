using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DELETERealtionMedidoresCompany2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LigtherMetric_ElectriCompany_ElectriCompanyId",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropIndex(
                name: "IX_LigtherMetric_ElectriCompanyId",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "ElectriCompanyId",
                schema: "mup",
                table: "LigtherMetric");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ElectriCompanyId",
                schema: "mup",
                table: "LigtherMetric",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LigtherMetric_ElectriCompanyId",
                schema: "mup",
                table: "LigtherMetric",
                column: "ElectriCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_LigtherMetric_ElectriCompany_ElectriCompanyId",
                schema: "mup",
                table: "LigtherMetric",
                column: "ElectriCompanyId",
                principalSchema: "mup",
                principalTable: "ElectriCompany",
                principalColumn: "Id");
        }
    }
}
