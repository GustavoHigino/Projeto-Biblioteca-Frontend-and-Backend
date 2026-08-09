using projetobiblioteca.Data;
using projetobiblioteca.Model;
using projetobiblioteca.Repositorios;

namespace projetobiblioteca.Servicos.Implementation
{
    public class EmprestimoFuncionarioService : GenericService<EmprestimoFuncionario>, IEmprestimoFuncionarioService,IGenericService<EmprestimoFuncionario>
    {
        private readonly IEmprestimoFuncionarioRepository _repositoryEmprestimoFuncionario;
        public EmprestimoFuncionarioService(IEmprestimoFuncionarioRepository repositoryEmprestimoFuncionario,
            IGenericRepository<EmprestimoFuncionario> repository) : base(repository)
        {
            _repositoryEmprestimoFuncionario = repositoryEmprestimoFuncionario;
        }


        

        public EmprestimoFuncionario Returned(long id)
        {
            return _repositoryEmprestimoFuncionario.Returned(id);
        }
        public EmprestimoFuncionario NotReturned(long id)
        {
            return _repositoryEmprestimoFuncionario.NotReturned(id);
        }
    }
}
