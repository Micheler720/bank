using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Clients.Api.Migrations
{
    /// <inheritdoc />
    public partial class IncluindoTabelaClientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "clients",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: true),
                    birth_date = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    document = table.Column<string>(type: "varchar(100)", nullable: true),
                    email = table.Column<string>(type: "varchar(100)", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    update_date = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clients",
                schema: "public");
        }
    }
}
