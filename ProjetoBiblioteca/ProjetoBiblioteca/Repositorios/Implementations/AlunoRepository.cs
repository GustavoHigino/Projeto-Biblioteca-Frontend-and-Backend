using Microsoft.EntityFrameworkCore;
using projetobiblioteca.Context;
using projetobiblioteca.Data;
using projetobiblioteca.Model;
using projetobiblioteca.Pagination;

namespace projetobiblioteca.Repositorios.Implementations
{
    public class AlunoRepository : GenericRepository<Aluno>, IAlunoRepository
    {
        
        public AlunoRepository(MSSQL context) : base(context)
        {
        }
        

        public Aluno Disable(long id)
        {
            var foundEntity = ShowById(id);
            if (foundEntity == null)
            {
                return null;
            }
            foundEntity.Habilitado = false;
            _context.SaveChanges();
            return foundEntity;
        }

        public Aluno Enable(long id)
        {
            var foundEntity = ShowById(id);
            if (foundEntity == null)
            {
                return null;
            }
            foundEntity.Habilitado = true;
            _context.SaveChanges();
            return foundEntity;
        }

    }
}
