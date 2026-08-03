using System.ComponentModel.DataAnnotations;

namespace projetobiblioteca.Data.DTO
{
    public class FuncionarioEmprestimoDTO
    {
        public string Nome { get; set; }
        public string Função { get; set; }
        public string Telefone { get; set; }
        public string Endereço { get; set; }
        public string Email { get; set; }
        public DateTime Nascimento { get; set; }

    }
}
