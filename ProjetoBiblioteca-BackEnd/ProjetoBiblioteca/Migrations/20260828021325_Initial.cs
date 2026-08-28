using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projetobiblioteca.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "alunos",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Curso = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Genero = table.Column<string>(type: "varchar(9)", maxLength: 9, nullable: false),
                    Endereço = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false),
                    Email = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    Nascimento = table.Column<DateTime>(type: "date", nullable: false),
                    Emprestimos = table.Column<int>(type: "int", nullable: false),
                    Habilitado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alunos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "funcionarios",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Função = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Genero = table.Column<string>(type: "varchar(9)", maxLength: 9, nullable: false),
                    Telefone = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false),
                    Endereço = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    Nascimento = table.Column<DateTime>(type: "date", nullable: false),
                    Emprestimos = table.Column<int>(type: "int", nullable: false),
                    Habilitado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_funcionarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "livros",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Autor = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Estoque = table.Column<long>(type: "bigint", nullable: false),
                    Titulo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Emprestados = table.Column<long>(type: "bigint", nullable: false),
                    Disponiveis = table.Column<long>(type: "bigint", nullable: false),
                    Habilitado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_livros", x => x.id);
                });

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
                    Enable = table.Column<bool>(type: "bit", nullable: false),
                    Key = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "EmprestimosAlunos",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAluno = table.Column<long>(type: "bigint", nullable: false),
                    IdLivro = table.Column<long>(type: "bigint", nullable: false),
                    Inicio = table.Column<DateTime>(type: "date", nullable: false),
                    Fim = table.Column<DateTime>(type: "date", nullable: false),
                    Devolvido = table.Column<bool>(type: "bit", nullable: false),
                    ValorMulta = table.Column<int>(type: "int", nullable: false),
                    Multado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmprestimosAlunos", x => x.id);
                    table.ForeignKey(
                        name: "FK_EmprestimosAlunos_alunos_IdAluno",
                        column: x => x.IdAluno,
                        principalTable: "alunos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmprestimosAlunos_livros_IdLivro",
                        column: x => x.IdLivro,
                        principalTable: "livros",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmprestimosFuncionarios",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFuncionario = table.Column<long>(type: "bigint", nullable: false),
                    IdLivro = table.Column<long>(type: "bigint", nullable: false),
                    Inicio = table.Column<DateTime>(type: "date", nullable: false),
                    Fim = table.Column<DateTime>(type: "date", nullable: false),
                    Devolvido = table.Column<bool>(type: "bit", nullable: false),
                    ValorMulta = table.Column<int>(type: "int", nullable: false),
                    Multado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmprestimosFuncionarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_EmprestimosFuncionarios_funcionarios_IdFuncionario",
                        column: x => x.IdFuncionario,
                        principalTable: "funcionarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmprestimosFuncionarios_livros_IdLivro",
                        column: x => x.IdLivro,
                        principalTable: "livros",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmprestimosAlunos_IdAluno",
                table: "EmprestimosAlunos",
                column: "IdAluno");

            migrationBuilder.CreateIndex(
                name: "IX_EmprestimosAlunos_IdLivro",
                table: "EmprestimosAlunos",
                column: "IdLivro");

            migrationBuilder.CreateIndex(
                name: "IX_EmprestimosFuncionarios_IdFuncionario",
                table: "EmprestimosFuncionarios",
                column: "IdFuncionario");

            migrationBuilder.CreateIndex(
                name: "IX_EmprestimosFuncionarios_IdLivro",
                table: "EmprestimosFuncionarios",
                column: "IdLivro");

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
                name: "EmprestimosAlunos");

            migrationBuilder.DropTable(
                name: "EmprestimosFuncionarios");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "alunos");

            migrationBuilder.DropTable(
                name: "funcionarios");

            migrationBuilder.DropTable(
                name: "livros");
        }
    }
}
