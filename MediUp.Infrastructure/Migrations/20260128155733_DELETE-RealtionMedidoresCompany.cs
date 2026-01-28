using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DELETERealtionMedidoresCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LigtherMetric_ElectriCompany_ElectricCompanyId",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropIndex(
                name: "IX_LigtherMetric_ElectricCompanyId",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "ElectricCompanyId",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "AuthorizationDocument",
                schema: "mup",
                table: "ElectriCompany");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<long>(
                name: "ElectricCompanyId",
                schema: "mup",
                table: "LigtherMetric",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "AuthorizationDocument",
                schema: "mup",
                table: "ElectriCompany",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LigtherMetric_ElectricCompanyId",
                schema: "mup",
                table: "LigtherMetric",
                column: "ElectricCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_LigtherMetric_ElectriCompany_ElectricCompanyId",
                schema: "mup",
                table: "LigtherMetric",
                column: "ElectricCompanyId",
                principalSchema: "mup",
                principalTable: "ElectriCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
