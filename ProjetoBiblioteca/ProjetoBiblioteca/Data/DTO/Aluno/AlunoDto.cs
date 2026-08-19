using System.Text.Json.Serialization;

namespace projetobiblioteca.Data.DTO.Aluno
{
    public record AlunoDto([property: JsonIgnore] long Id,string Nome, string Curso, string Genero,
        string Endereço, string Telefone, string Email, DateTime Nascimento
        )
    {
        
    }
}
