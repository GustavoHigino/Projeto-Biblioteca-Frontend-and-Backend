using projetobiblioteca.Data;

namespace projetobiblioteca.Repositorios
{
    public interface IAlunoRepository : IGenericRepository<Fundionario>
    {
        Fundionario Enable(long id);
        Fundionario Disable(long id);


    }
}
