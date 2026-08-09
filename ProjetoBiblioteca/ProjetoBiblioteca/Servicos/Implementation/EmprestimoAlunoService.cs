using projetobiblioteca.Data;
using projetobiblioteca.Model;
using projetobiblioteca.Repositorios;

namespace projetobiblioteca.Servicos.Implementation
{
    public class EmprestimoAlunoService : GenericService<EmprestimoAlunos>, IEmprestimoAlunoService, IGenericService<EmprestimoAlunos>
    {
        private readonly IEmprestimoAlunoRepository _repositoryEmprestimoAluno;
        public EmprestimoAlunoService(IEmprestimoAlunoRepository repositoryEmprestimoAluno
            , IGenericRepository<EmprestimoAlunos> repository) : base(repository)
        {
            _repositoryEmprestimoAluno = repositoryEmprestimoAluno;
        }
        
        public EmprestimoAlunos Returned(long id)
        {
            return _repositoryEmprestimoAluno.Returned(id);
        }

        public EmprestimoAlunos NotReturned(long id)
        {
            return _repositoryEmprestimoAluno.NotReturned(id);
        }


    }
}
