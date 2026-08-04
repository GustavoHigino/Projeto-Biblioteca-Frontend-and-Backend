using projetobiblioteca.Context;
using projetobiblioteca.Data;

namespace projetobiblioteca.Repositorios.Implementations
{
    public class LivroRepository : GenericRepository<Livro>, ILivroRepository
    {
        public LivroRepository(MSSQL context) : base(context)
        {
        }
    }
}
