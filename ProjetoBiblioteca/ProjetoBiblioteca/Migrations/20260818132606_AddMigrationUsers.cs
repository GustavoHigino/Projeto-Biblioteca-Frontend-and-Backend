using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projetobiblioteca.Migrations
{
    /// <inheritdoc />
    public partial class AddMigrationUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "livros",
                newName: "Titulo");

            migrationBuilder.AddColumn<long>(
                name: "Disponiveis",
                table: "livros",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Fullname = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    passwordhash = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    refreshtoken = table.Column<string>(type: "varchar(300)", maxLength: 500, nullable: true),
                    refreshtokenexpirytime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Enable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropColumn(
                name: "Disponiveis",
                table: "livros");

            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "livros",
                newName: "Nome");
        }
    }
}
