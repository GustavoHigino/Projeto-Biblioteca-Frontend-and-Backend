using projetobiblioteca.Context;
using projetobiblioteca.Data;

namespace projetobiblioteca.Repositorios.Implementations
{
    public class FuncionarioRepository : GenericRepository<Funcionario>, IFuncionarioRepository 
    {
        public FuncionarioRepository(MSSQL context) : base(context)
        {
        }
    }
}
