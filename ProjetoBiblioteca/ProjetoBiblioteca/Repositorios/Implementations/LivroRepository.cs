using projetobiblioteca.Context;
using projetobiblioteca.Data;
using projetobiblioteca.Model;

namespace projetobiblioteca.Repositorios.Implementations
{
    public class LivroRepository : GenericRepository<Livro>, ILivroRepository
    {
        public LivroRepository(MSSQL context) : base(context)
        {
        }

        public Livro Disable(long id)
        {
            var findedEntity=ShowById(id);
            if (findedEntity == null)
            {
                return null;
            }
            findedEntity.Habilitado = false;
            _context.SaveChanges();
            return findedEntity;
        }

        public Livro Enable(long id)
        {
            var findedEntity = ShowById(id);
            if(findedEntity == null)
            {
                return null;
            }
            findedEntity.Habilitado = true;
            _context.SaveChanges();
            return findedEntity;
        }
    }
}
