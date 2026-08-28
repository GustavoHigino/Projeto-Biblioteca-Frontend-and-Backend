using Mapster;
using Microsoft.EntityFrameworkCore;
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
            var query = Show().AsQueryable().AsNoTracking().Include(a=> a.EmprestimoFuncionario).ThenInclude(a=> a.Livro);
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

        public Funcionario FindByIdQuery(long id)
        {
            return Show().AsQueryable().AsNoTracking()
                .Include(f => f.EmprestimoFuncionario)
                .ThenInclude(e => e.Livro)
                .FirstOrDefault(p => p.Id == id);
        }
    }
}
