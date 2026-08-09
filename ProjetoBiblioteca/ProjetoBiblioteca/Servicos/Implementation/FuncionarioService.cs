using projetobiblioteca.Data;
using projetobiblioteca.Repositorios;

namespace projetobiblioteca.Servicos.Implementation
{
    public class FuncionarioService : GenericService<Funcionario>, IFuncionarioService, IGenericService<Funcionario>
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
       
        public FuncionarioService(IFuncionarioRepository funcionarioRepository,
            IGenericRepository<Funcionario> repositoryGeneric) : base(repositoryGeneric) 
        {
            _funcionarioRepository = funcionarioRepository;
        }
        
        public Funcionario Enable(long id)
        {
            return _funcionarioRepository.Enable(id);
        }
        public Funcionario Disable(long id)
        {
            return _funcionarioRepository.Disable(id);
        }
    }
}
