using projetobiblioteca.Model;
using projetobiblioteca.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projetobiblioteca.Data
{
    [Table("alunos")]
    public class Aluno : ModeloBase
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Curso { get; set; }
        [Required]
        public string Genero { get; set; }
        [Required]
        public string Endereço { get; set; }
        [Required]
        public string Telefone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime Nascimento { get; set; }

        public bool Habilitado { get; set; } = true;
        public ICollection<EmprestimoAluno> EmprestimosAluno { get; set; } = new List<EmprestimoAluno>();

    }
}
