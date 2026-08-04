using projetobiblioteca.Model;

namespace projetobiblioteca.Repositorios
{
    public interface IEmprestimoFuncionarioRepository : IGenericRepository<EmprestimoFuncionario>
    {
        EmprestimoFuncionario Devolvido(long id);
    }
}
