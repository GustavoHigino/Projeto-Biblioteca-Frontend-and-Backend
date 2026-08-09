using projetobiblioteca.Data;
using projetobiblioteca.Pagination;

namespace projetobiblioteca.Repositorios
{
    public interface IFuncionarioRepository : IGenericRepository<Funcionario>
    {
        Funcionario Enable(long id);
        Funcionario Disable(long id);
    }
}
