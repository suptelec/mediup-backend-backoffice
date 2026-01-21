using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLgthers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlCertificate",
                schema: "mup",
                table: "LigtherTransformers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UrlCertificate",
                schema: "mup",
                table: "LigtherMetric",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AuthorizationDocument",
                schema: "mup",
                table: "ElectriCompany",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlCertificate",
                schema: "mup",
                table: "LigtherTransformers");

            migrationBuilder.DropColumn(
                name: "UrlCertificate",
                schema: "mup",
                table: "LigtherMetric");

            migrationBuilder.DropColumn(
                name: "AuthorizationDocument",
                schema: "mup",
                table: "ElectriCompany");
        }
    }
}
