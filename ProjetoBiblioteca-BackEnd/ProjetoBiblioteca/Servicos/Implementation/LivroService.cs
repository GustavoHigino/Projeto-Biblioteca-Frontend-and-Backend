using Mapster;
using Microsoft.EntityFrameworkCore;
using projetobiblioteca.Data;
using projetobiblioteca.Data.DTO.Livro;
using projetobiblioteca.Model;
using projetobiblioteca.Pagination;
using projetobiblioteca.Repositorios;

namespace projetobiblioteca.Servicos.Implementation
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _repositoryLivro;
        public LivroService(ILivroRepository repositoryLivro,
            IGenericRepository<Livro> repository) 
        {
            _repositoryLivro = repositoryLivro;
        }
        
        public Livro Enable(long id)
        {
            return _repositoryLivro.Enable(id);
        }

        public Livro Disable(long id)
        {
            return _repositoryLivro.Disable(id);
        }

        public Task<PaginationClass<Livro>> PagedList(int ItensPage, long pageCurrently)
        {
            var query = Show().AsQueryable().AsNoTracking()
                .Include(a => a.EmprestimosAluno).Include(a => a.EmprestimosFuncionario);
            return _repositoryLivro.PagedList(ItensPage, pageCurrently);
        }

        public IQueryable<Livro> Show()
        {
            return _repositoryLivro.Show();
        }

        public Livro ShowById(long id)
        {
            return _repositoryLivro.ShowById(id);
        }

        public LivroDto Add(LivroDto accept)
        {
            var livro = accept.Adapt<Livro>();
            livro.Disponiveis = livro.Estoque - livro.Emprestados;
            return _repositoryLivro.Add(livro).Adapt<LivroDto>();
        }

        public LivroDto Update(LivroDto accept)
        {
            var livro = accept.Adapt<Livro>();
            return _repositoryLivro.Update(livro).Adapt<LivroDto>();
        }

        public Livro FindByIdQuery(long id)
        {
            return Show().AsNoTracking().AsQueryable()
                .Include(l => l.EmprestimosAluno)
                .Include(l => l.EmprestimosFuncionario)
                .FirstOrDefault(l => l.Id == id);
        }
    }
}
