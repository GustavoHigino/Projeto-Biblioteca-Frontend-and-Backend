using projetobiblioteca.Context;
using projetobiblioteca.Data;

namespace projetobiblioteca.Repositorios.Implementations
{
    public class AlunoRepository : GenericRepository<Fundionario>, IAlunoRepository
    {
        
        public AlunoRepository(MSSQL context) : base(context)
        {
        }

        public bool EnableOrDisable(bool enableOrDisable)
        {
            Sho
        }
    }
}
