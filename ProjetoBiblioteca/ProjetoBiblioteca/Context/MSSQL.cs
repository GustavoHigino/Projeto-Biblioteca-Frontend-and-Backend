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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<EmprestimoAluno>()
                .HasOne(e => e.Livro)
                .WithMany(e => e.EmprestimosAluno)
                .HasForeignKey(e => e.IdLivro)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmprestimoFuncionario>()
                .HasOne(e => e.Livro)
                .WithMany(e => e.EmprestimosFuncionario)
                .HasForeignKey(e => e.IdFuncionario)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmprestimoAluno>()
                .HasOne(e => e.Aluno)
                .WithMany(e => e.EmprestimosAluno)
                .HasForeignKey(e => e.IdAluno)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmprestimoFuncionario>()
                .HasOne(e => e.Funcionario)
                .WithMany(e => e.EmprestimoFuncionario)
                .HasForeignKey(e => e.IdFuncionario)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
