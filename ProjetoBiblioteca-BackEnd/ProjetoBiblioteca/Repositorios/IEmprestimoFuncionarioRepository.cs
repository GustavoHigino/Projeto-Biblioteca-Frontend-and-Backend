using projetobiblioteca.Model;
using projetobiblioteca.Pagination;

namespace projetobiblioteca.Repositorios
{
    public interface IEmprestimoFuncionarioRepository : IGenericRepository<EmprestimoFuncionario>
    {
        EmprestimoFuncionario Returned(long id);
        EmprestimoFuncionario NotReturned(long id);
    }
}
