using projetobiblioteca.Data;
using projetobiblioteca.Pagination;

namespace projetobiblioteca.Repositorios
{
    public interface IAlunoRepository : IGenericRepository<Aluno>
    {
        Aluno Enable(long id);
        Aluno Disable(long id);


    }
}
