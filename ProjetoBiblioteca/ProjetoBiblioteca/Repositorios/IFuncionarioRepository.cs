using projetobiblioteca.Data;

namespace projetobiblioteca.Repositorios
{
    public interface IFuncionarioRepository : IGenericRepository<Funcionario>
    {
        Fundionario Enable(long id);
        Fundionario Disable(long id);
    }
}
