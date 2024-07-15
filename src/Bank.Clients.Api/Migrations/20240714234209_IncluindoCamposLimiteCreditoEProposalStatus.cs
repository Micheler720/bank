using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Clients.Api.Migrations
{
    /// <inheritdoc />
    public partial class IncluindoCamposLimiteCreditoEProposalStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "credit_limit",
                schema: "public",
                table: "clients",
                type: "numeric(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "proposal_status",
                schema: "public",
                table: "clients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "credit_limit",
                schema: "public",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "proposal_status",
                schema: "public",
                table: "clients");
        }
    }
}
