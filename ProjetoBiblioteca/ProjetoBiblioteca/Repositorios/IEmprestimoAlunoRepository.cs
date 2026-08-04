using projetobiblioteca.Model;

namespace projetobiblioteca.Repositorios
{
    public interface IEmprestimoAlunoRepository : IGenericRepository<EmprestimoAluno>
    {
        EmprestimoAluno Devolvido(long id);
        
    }
}
