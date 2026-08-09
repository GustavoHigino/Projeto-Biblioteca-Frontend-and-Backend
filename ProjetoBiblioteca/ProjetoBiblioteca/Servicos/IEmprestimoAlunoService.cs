using projetobiblioteca.Model;

namespace projetobiblioteca.Servicos
{
    public interface IEmprestimoAlunoService : IGenericService<EmprestimoAlunos>
    {
        
        EmprestimoAlunos Returned(long id);
        EmprestimoAlunos NotReturned(long id);
    }
}
