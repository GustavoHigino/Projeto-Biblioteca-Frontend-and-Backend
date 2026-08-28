using projetobiblioteca.HATEOAS.Abstract;
using projetobiblioteca.HATEOAS.Filters;
using projetobiblioteca.Model;
using projetobiblioteca.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projetobiblioteca.Model
{
    [Table("alunos")]
    public class Aluno : ModeloBase,ISupportsHypermedia
    {
        [Required]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Nome { get; set; }
        [Required]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Curso { get; set; }
        [Required]
        [MaxLength(9)]
        [Column(TypeName ="varchar(9)")]
        public string Genero { get; set; }
        [Required]
        [MaxLength(100)]
        [Column(TypeName ="varchar(100)")]
        public string Endereço { get; set; }
        [Required]
        [MaxLength(12)]
        [Column(TypeName ="varchar(12)")]
        public string Telefone { get; set; }
        [Required]
        [MaxLength(80)]
        [Column(TypeName = "varchar(80)")]
        public string Email { get; set; }
        [Required]
        [Column(TypeName ="date")]//"1995-05-20"
        public DateTime Nascimento { get; set; }
        [Column]
        public int Emprestimos { get; set; } = 0;

        
        public bool Habilitado { get; set; } = true;
        public List<HypermediaLink> Links { get; set; } = [];
        public ICollection<EmprestimoAlunos> EmprestimosAluno { get; set; } = new List<EmprestimoAlunos>();

    }
}
