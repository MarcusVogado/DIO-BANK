using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DIO_BANK.Migrations
{
    /// <inheritdoc />
    public partial class BankDbInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TipoConta = table.Column<int>(type: "INTEGER", nullable: false),
                    Saldo = table.Column<double>(type: "REAL", nullable: false),
                    Credito = table.Column<double>(type: "REAL", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contas");
        }
    }
}
