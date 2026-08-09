using projetobiblioteca.Context;
using projetobiblioteca.Data;

namespace projetobiblioteca.Repositorios.Implementations
{
    public class FuncionarioRepository : GenericRepository<Funcionario>, IFuncionarioRepository 
    {
        public FuncionarioRepository(MSSQL context) : base(context)
        {
        }

        public Funcionario Disable(long id)
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

        public Funcionario Enable(long id)
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
