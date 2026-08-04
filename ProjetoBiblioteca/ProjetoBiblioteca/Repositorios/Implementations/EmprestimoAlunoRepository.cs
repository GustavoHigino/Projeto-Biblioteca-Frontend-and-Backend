using projetobiblioteca.Context;
using projetobiblioteca.Model;

namespace projetobiblioteca.Repositorios.Implementations
{
    public class EmprestimoAlunoRepository : GenericRepository<EmprestimoAluno>, IEmprestimoAlunoRepository
    {
        public EmprestimoAlunoRepository(MSSQL context) : base(context)
        {
        }
    }
}
