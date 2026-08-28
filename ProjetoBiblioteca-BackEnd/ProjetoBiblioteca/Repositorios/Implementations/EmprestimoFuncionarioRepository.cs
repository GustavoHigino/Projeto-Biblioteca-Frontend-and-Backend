using projetobiblioteca.Context;
using projetobiblioteca.Model;

namespace projetobiblioteca.Repositorios.Implementations
{
    public class EmprestimoFuncionarioRepository : GenericRepository<EmprestimoFuncionario>, IEmprestimoFuncionarioRepository
    {
        public EmprestimoFuncionarioRepository(MSSQL context) : base(context)
        {
        }

        public EmprestimoFuncionario NotReturned(long id)
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

        public EmprestimoFuncionario Returned(long id)
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
