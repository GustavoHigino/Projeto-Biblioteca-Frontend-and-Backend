using System.Text.Json.Serialization;

namespace projetobiblioteca.Data.DTO.EmprestimoAluno
{
    public record EmprestimoAlunoDto
        ([property: JsonIgnore] long Id, long IdAluno,long IdLivro)
    {
    }
}
