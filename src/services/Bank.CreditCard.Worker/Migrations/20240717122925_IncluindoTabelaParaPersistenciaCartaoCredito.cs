using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.CreditCard.Worker.Migrations
{
    /// <inheritdoc />
    public partial class IncluindoTabelaParaPersistenciaCartaoCredito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "credit_cards",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    credit_limit = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    card_number = table.Column<string>(type: "varchar(100)", nullable: false),
                    security_code = table.Column<string>(type: "varchar(10)", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_credit_cards", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "credit_cards",
                schema: "public");
        }
    }
}
