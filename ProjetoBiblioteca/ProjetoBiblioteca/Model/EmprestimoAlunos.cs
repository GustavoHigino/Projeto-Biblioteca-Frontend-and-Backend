using projetobiblioteca.Data;
using projetobiblioteca.Data.DTO;
using projetobiblioteca.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace projetobiblioteca.Model
{
    [Table("EmprestimosAlunos")]
    public class EmprestimoAlunos : ModeloBase
    {
        [Required]
        public long IdAluno { get; set; }
        [Required]
        public long IdLivro { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime Inicio { get; set; } = DateTime.UtcNow;
        [Required]
        [Column(TypeName = "date")]
        public DateTime Fim { get; set; } = DateTime.UtcNow.AddDays(7);
        [Required]
        public bool Devolvido { get; set; } = false;
        [ForeignKey(nameof(IdAluno))]
        
        public Aluno Aluno { get; set; }
        [ForeignKey(nameof(IdLivro))]
        public Livro Livro { get; set; }
    }
}
