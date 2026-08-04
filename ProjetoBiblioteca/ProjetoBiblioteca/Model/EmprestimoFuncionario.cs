using projetobiblioteca.Data;
using projetobiblioteca.Data.DTO;
using projetobiblioteca.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projetobiblioteca.Model
{
    [Table("EmprestimosFuncionarios")]
    public class EmprestimoFuncionario : ModeloBase
    {
        [Required]

        public long IdFuncionario { get; set; }
        [Required]
        public long IdLivro { get; set; }
        [Required]
        [Column(TypeName ="date")]
        public DateTime Inicio { get; set; }
        [Required]
        [Column(TypeName ="date")]
        public DateTime Fim { get; set; }
        [Required]
        public bool Devolvido { get; set; } = false;
        [ForeignKey(nameof(IdFuncionario))]
        public Funcionario Funcionario{  get; set; }
        [ForeignKey(nameof(IdLivro))]
        public Livro Livro{  get; set; }

    }
}
