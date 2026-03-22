using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Noblobs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PdfContent",
                schema: "mup",
                table: "MaintenanceFilesHistories");

            migrationBuilder.RenameColumn(
                name: "HtmlContent",
                schema: "mup",
                table: "MaintenanceFilesHistories",
                newName: "PdfContentUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PdfContentUrl",
                schema: "mup",
                table: "MaintenanceFilesHistories",
                newName: "HtmlContent");

            migrationBuilder.AddColumn<byte[]>(
                name: "PdfContent",
                schema: "mup",
                table: "MaintenanceFilesHistories",
                type: "longblob",
                nullable: true);
        }
    }
}
