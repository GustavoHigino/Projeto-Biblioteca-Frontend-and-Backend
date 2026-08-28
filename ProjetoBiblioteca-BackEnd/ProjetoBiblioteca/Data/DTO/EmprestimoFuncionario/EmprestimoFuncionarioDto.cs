using System.Text.Json.Serialization;

namespace projetobiblioteca.Data.DTO.EmprestimoFuncionario
{
    public record EmprestimoFuncionarioDto
        ([property: JsonIgnore] long Id, long IdFuncionario, long IdLivro)
    {
    }
}
