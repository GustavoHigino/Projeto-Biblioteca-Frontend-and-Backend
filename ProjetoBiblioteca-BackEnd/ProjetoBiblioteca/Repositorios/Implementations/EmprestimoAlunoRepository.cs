using Microsoft.EntityFrameworkCore;
using projetobiblioteca.Context;
using projetobiblioteca.Model;
using projetobiblioteca.Pagination;

namespace projetobiblioteca.Repositorios.Implementations
{
    public class EmprestimoAlunoRepository : GenericRepository<EmprestimoAlunos>, IEmprestimoAlunoRepository
    {
        public EmprestimoAlunoRepository(MSSQL context) : base(context)
        {
        }
        
        

        public EmprestimoAlunos NotReturned(long id)
        {
            var foundEntity = ShowById(id);
            if (foundEntity == null)
            {
                return null;
            }
            foundEntity.Devolvido = false;
            _context.SaveChanges();
            return foundEntity;
        }


        public EmprestimoAlunos Returned(long id)
        {
            var foundEntity = ShowById(id);
            if (foundEntity == null)
            {
                return null;
            }
            foundEntity.Devolvido = true;
            _context.SaveChanges();
            return foundEntity;
        }
    }
}
