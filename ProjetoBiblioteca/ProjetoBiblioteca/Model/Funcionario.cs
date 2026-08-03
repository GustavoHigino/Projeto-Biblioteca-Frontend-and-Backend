using projetobiblioteca.Model;
using projetobiblioteca.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projetobiblioteca.Data
{
    [Table("funcionarios")]
    public class Funcionario : ModeloBase
    {
        [Required]
        public string Nome {  get; set; }
        [Required]
        public string Função {  get; set; }
        [Required]
        public string Genero { get; set; }
        [Required]
        public string Telefone { get; set; }
        [Required]
        public string Endereço {  get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime Nascimento { get; set; }

        public bool Habilitado { get; set; } = true;
        public ICollection<EmprestimoFuncionario> EmprestimoFuncionario
        { get; set; } = new List<EmprestimoFuncionario>();
        
    }
}
