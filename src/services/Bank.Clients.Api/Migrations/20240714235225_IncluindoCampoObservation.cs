using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Clients.Api.Migrations
{
    /// <inheritdoc />
    public partial class IncluindoCampoObservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "observation",
                schema: "public",
                table: "clients",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "observation",
                schema: "public",
                table: "clients");
        }
    }
}
