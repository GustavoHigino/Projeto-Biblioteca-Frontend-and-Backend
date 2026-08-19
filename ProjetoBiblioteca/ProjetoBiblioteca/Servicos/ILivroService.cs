using projetobiblioteca.Data;
using projetobiblioteca.Data.DTO.Livro;
using projetobiblioteca.Model;
using projetobiblioteca.Pagination;

namespace projetobiblioteca.Servicos
{
    public interface ILivroService 
    {
        Task<PaginationClass<Livro>> PagedList(int ItensPage, long pageCurrently);
        IQueryable<Livro> Show();
        Livro ShowById(long id);
        public LivroDto Add(LivroDto accept);
        public LivroDto Update(LivroDto accept);
        Livro Enable(long id);
        Livro Disable(long id);
    }
}
