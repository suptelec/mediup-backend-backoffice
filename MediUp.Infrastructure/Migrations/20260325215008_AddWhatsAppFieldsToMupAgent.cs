using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWhatsAppFieldsToMupAgent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WhatsAppApiKey",
                schema: "mup",
                table: "Agents",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "WhatsAppEnabled",
                schema: "mup",
                table: "Agents",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WhatsAppApiKey",
                schema: "mup",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "WhatsAppEnabled",
                schema: "mup",
                table: "Agents");
        }
    }
}
