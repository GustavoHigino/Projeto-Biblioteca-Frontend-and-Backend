namespace projetobiblioteca.Data.DTO.Aluno
{
    public record AlunoDto(string Nome, string Curso, string Genero,
        string Endereço, string Telefone, string Email, DateTime Nascimento
        )
    {
        
    }
}
