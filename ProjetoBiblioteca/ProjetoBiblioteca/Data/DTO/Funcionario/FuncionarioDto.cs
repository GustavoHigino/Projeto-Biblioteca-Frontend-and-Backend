using System.Text.Json.Serialization;

namespace projetobiblioteca.Data.DTO.Funcionario
{
    public record FuncionarioDto
        ([property: JsonIgnore] long Id, string Nome, string Função, string Genero, string Telefone,
        string Endereço, string Email, DateTime Nascimento)
    {
    }
}
