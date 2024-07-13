using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Clients.Api.Migrations
{
    /// <inheritdoc />
    public partial class AjustandoNomeColunaUpdatedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "update_date",
                schema: "public",
                table: "clients",
                newName: "updated_date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updated_date",
                schema: "public",
                table: "clients",
                newName: "update_date");
        }
    }
}
