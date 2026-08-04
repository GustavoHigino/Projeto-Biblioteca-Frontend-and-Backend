using projetobiblioteca.Model;
using projetobiblioteca.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projetobiblioteca.Data
{
    [Table("livros")]
    public class Livro : ModeloBase
    {
        [Required]
        [MaxLength(100)]
        [Column(TypeName ="varchar(100)")]
        public string Autor { get; set; }
        [Required]
        public long Estoque {  get; set; }
        [Required]
        [MaxLength(100)]
        [Column(TypeName ="varchar(100)")]
        public string Nome { get; set; }
        [Required]

        public long Emprestados { get; set; }
        
        public bool Habilitado { get; set; } = true;
        public ICollection<EmprestimoAluno> EmprestimosAluno 
        { get; set; } = new List<EmprestimoAluno>();
        public ICollection<EmprestimoFuncionario> EmprestimosFuncionario
        { get; set; } = new List<EmprestimoFuncionario>();

        
    }
}
