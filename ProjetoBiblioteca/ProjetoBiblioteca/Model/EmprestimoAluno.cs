using projetobiblioteca.Data;
using projetobiblioteca.Data.DTO;
using projetobiblioteca.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projetobiblioteca.Model
{
    [Table("EmprestimosAlunos")]
    public class EmprestimoAluno : ModeloBase
    {
        [Required]
        public int IdAluno { get; set; }
        [Required]
        public int IdLivro { get; set; }
        [Required]
        public DateTime Inicio { get; set; }
        [Required]
        public DateTime Fim { get; set; }
        [Required]
        public bool Devolvido { get; set; } = false;
        [ForeignKey(nameof(IdAluno))]
        public Aluno Aluno { get; set; }
        [ForeignKey(nameof(IdLivro))]
        public Livro Livro { get; set; }
    }
}
