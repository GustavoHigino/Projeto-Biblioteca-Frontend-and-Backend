using Mapster;
using projetobiblioteca.Data;
using projetobiblioteca.Data.DTO.Funcionario;
using projetobiblioteca.Model;
using projetobiblioteca.Pagination;
using projetobiblioteca.Repositorios;

namespace projetobiblioteca.Servicos.Implementation
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
       
        public FuncionarioService(IFuncionarioRepository funcionarioRepository)
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

        public Task<PaginationClass<Funcionario>> PagedList(int ItensPage, long pageCurrently)
        {
            return _funcionarioRepository.PagedList(ItensPage, pageCurrently);
        }

        public IQueryable<Funcionario> Show()
        {
            return _funcionarioRepository.Show();
        }

        public Funcionario ShowById(long id)
        {
            return _funcionarioRepository.ShowById(id);
        }

        public FuncionarioDto Add(FuncionarioDto accept)
        {
            var funcionario = accept.Adapt<Funcionario>();
            return _funcionarioRepository.Add(funcionario).Adapt<FuncionarioDto>();
        }

        public FuncionarioDto Update(FuncionarioDto accept)
        {
            var funcionario = accept.Adapt<Funcionario>();
            return _funcionarioRepository.Update(funcionario).Adapt<FuncionarioDto>();
        }
    }
}
