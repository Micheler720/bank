using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.CreditCard.Worker.Migrations
{
    /// <inheritdoc />
    public partial class IncluindoClientId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "client_id",
                schema: "public",
                table: "credit_cards",
                type: "varchar(36)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "client_id",
                schema: "public",
                table: "credit_cards");
        }
    }
}
