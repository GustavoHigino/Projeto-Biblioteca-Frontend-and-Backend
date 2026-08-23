using projetobiblioteca.Model;
using projetobiblioteca.Pagination;

namespace projetobiblioteca.Repositorios
{
    public interface IEmprestimoAlunoRepository : IGenericRepository<EmprestimoAlunos>
    {
        EmprestimoAlunos Returned(long id);
        EmprestimoAlunos NotReturned(long id);

    }
}
