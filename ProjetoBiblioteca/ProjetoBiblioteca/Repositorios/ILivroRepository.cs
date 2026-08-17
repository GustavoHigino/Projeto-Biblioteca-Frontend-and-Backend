using projetobiblioteca.Data;
using projetobiblioteca.Model;
using projetobiblioteca.Pagination;

namespace projetobiblioteca.Repositorios
{
    public interface ILivroRepository : IGenericRepository<Livro>
    {
        Livro Enable(long id);
        Livro Disable(long id);
    }
}
