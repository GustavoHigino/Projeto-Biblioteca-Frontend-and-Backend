using System.Text.Json.Serialization;

namespace projetobiblioteca.Data.DTO.Livro
{
    public record LivroDto
        ([property: JsonIgnore] long Id, string Autor, long Estoque, string Titulo, long Emprestados)
    {
    }
}
