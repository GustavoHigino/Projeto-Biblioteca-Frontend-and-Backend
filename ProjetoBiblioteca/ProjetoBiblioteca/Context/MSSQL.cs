using Microsoft.EntityFrameworkCore;
using projetobiblioteca.Data;
using projetobiblioteca.Model;

namespace projetobiblioteca.Context
{
    public class MSSQL : DbContext
    {
        public MSSQL(DbContextOptions<MSSQL> options) : base(options)
        {
            
        }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<EmprestimoAluno> EmpAluno { get; set; }
        public DbSet<EmprestimoFuncionario> EmpFuncionario { get; set; }
    }
}
