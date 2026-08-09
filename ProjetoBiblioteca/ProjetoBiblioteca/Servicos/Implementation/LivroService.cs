using projetobiblioteca.Data;
using projetobiblioteca.Repositorios;

namespace projetobiblioteca.Servicos.Implementation
{
    public class LivroService : GenericService<Livro>, ILivroService,IGenericService<Livro>
    {
        private readonly ILivroRepository _repositoryLivro;
        public LivroService(ILivroRepository repositoryLivro,
            IGenericRepository<Livro> repository) : base(repository)
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


    }
}
