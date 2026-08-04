using projetobiblioteca.Data;

namespace projetobiblioteca.Repositorios
{
    public interface ILivroRepository : IGenericRepository<Livro>
    {
        Livro Enable(long id);
        Livro Disable(long id);
    }
}
