using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWhatsAppFieldsToMupAgenttwilo1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WhatsAppPhone",
                schema: "mup",
                table: "Agents",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WhatsAppPhone",
                schema: "mup",
                table: "Agents");
        }
    }
}
