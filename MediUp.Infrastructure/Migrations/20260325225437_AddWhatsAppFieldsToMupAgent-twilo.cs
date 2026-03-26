using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWhatsAppFieldsToMupAgenttwilo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WhatsAppApiKey",
                schema: "mup",
                table: "Agents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WhatsAppApiKey",
                schema: "mup",
                table: "Agents",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
