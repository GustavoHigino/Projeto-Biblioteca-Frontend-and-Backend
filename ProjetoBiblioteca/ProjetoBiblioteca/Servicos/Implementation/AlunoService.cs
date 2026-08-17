using projetobiblioteca.Data;
using projetobiblioteca.Model;
using projetobiblioteca.Pagination;
using projetobiblioteca.Repositorios;

namespace projetobiblioteca.Servicos.Implementation
{
    public class AlunoService : GenericService<Aluno>,IAlunoService, IGenericService<Aluno>

    {
        private readonly IAlunoRepository _alunoRepository;
        public AlunoService(IAlunoRepository alunoRepository,
            IGenericRepository<Aluno> repository
        ):base(repository)
        {
            _alunoRepository = alunoRepository;
        }
        

        public Aluno Enable(long id)
        {
            return _alunoRepository.Enable(id);
        }

        public Aluno Disable(long id)
        {
            return _alunoRepository.Disable(id);
        }

        
    }
}
