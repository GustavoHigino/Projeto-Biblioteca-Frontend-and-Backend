using projetobiblioteca.Context;
using projetobiblioteca.Model;

namespace projetobiblioteca.Repositorios.Implementations
{
    public class EmprestimoFuncionarioRepository : GenericRepository<EmprestimoFuncionario>, IEmprestimoFuncionarioRepository
    {
        public EmprestimoFuncionarioRepository(MSSQL context) : base(context)
        {
        }
    }
}
