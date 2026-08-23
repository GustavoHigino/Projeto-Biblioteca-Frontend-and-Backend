using Mapster;
using Microsoft.EntityFrameworkCore;
using projetobiblioteca.Data;
using projetobiblioteca.Data.DTO.EmprestimoFuncionario;
using projetobiblioteca.Data.DTO.Funcionario;
using projetobiblioteca.Data.DTO.Livro;
using projetobiblioteca.Model;
using projetobiblioteca.Pagination;
using projetobiblioteca.Repositorios;

namespace projetobiblioteca.Servicos.Implementation
{
    public class EmprestimoFuncionarioService :  IEmprestimoFuncionarioService
    {
        private readonly IEmprestimoFuncionarioRepository _repositoryEmprestimoFuncionario;
        private readonly ILivroService _livroService;
        private readonly IFuncionarioService _funcionarioService;
        public EmprestimoFuncionarioService(IEmprestimoFuncionarioRepository repositoryEmprestimoFuncionario,
            ILivroService livroService,
            IFuncionarioService funcionarioService) 
        {
            _repositoryEmprestimoFuncionario = repositoryEmprestimoFuncionario;
            _livroService = livroService;
            _funcionarioService = funcionarioService;
        }


        

        public EmprestimoFuncionario Returned(long id)
        {
            var returned= _repositoryEmprestimoFuncionario.Returned(id);
            if(returned.Devolvido==true)
            {
                return _repositoryEmprestimoFuncionario.Returned(id);
            }
            var funcionario = _funcionarioService.ShowById(returned.IdFuncionario);
            funcionario.Emprestimos = funcionario.Emprestimos + 1;
            _funcionarioService.Update(funcionario.Adapt<FuncionarioDto>());
            var livro=_livroService.ShowById(returned.IdLivro);
            livro.Disponiveis = livro.Disponiveis + 1;
            _livroService.Update(livro.Adapt<LivroDto>());
            return _repositoryEmprestimoFuncionario.Returned(id);


        }
        public EmprestimoFuncionario NotReturned(long id)
        {
            var emprestimo=_repositoryEmprestimoFuncionario.ShowById(id);
            if (emprestimo.Devolvido == false)
            {
                return _repositoryEmprestimoFuncionario.NotReturned(id);
            }
            var funcionario = _funcionarioService.ShowById(emprestimo.IdFuncionario);
            funcionario.Emprestimos = funcionario.Emprestimos - 1;
            _funcionarioService.Update(funcionario.Adapt<FuncionarioDto>());
            var livro = _livroService.ShowById(emprestimo.IdLivro);
            livro.Disponiveis = livro.Disponiveis - 1;
            _livroService.Update(livro.Adapt<LivroDto>());
            return _repositoryEmprestimoFuncionario.NotReturned(id);
        }

        public Task<PaginationClass<EmprestimoFuncionario>> PagedList(int ItensPage, long pageCurrently)
        {
            return _repositoryEmprestimoFuncionario.PagedList(ItensPage, pageCurrently);
        }

        public IQueryable<EmprestimoFuncionario> Show()
        {
            return _repositoryEmprestimoFuncionario.Show();
        }

        public EmprestimoFuncionario ShowById(long id)
        {
            return _repositoryEmprestimoFuncionario.ShowById(id);

        }

        public EmprestimoFuncionarioDto Add(EmprestimoFuncionarioDto accept)
        {
            var funcionario = _funcionarioService.ShowById(accept.Id);
            if (funcionario.Emprestimos > 2)
            {
                return null;
            }
            var livro=_livroService.ShowById(accept.IdLivro);
            if (livro.Disponiveis == 0)
            {
                return null;
            }
            funcionario.Emprestimos = funcionario.Emprestimos + 1;
            _funcionarioService.Update(funcionario.Adapt<FuncionarioDto>());
            var aluno=accept.Adapt<EmprestimoFuncionario>();
            livro.Disponiveis = livro.Disponiveis - 1;
            _livroService.Update(livro.Adapt<LivroDto>());
            
            return _repositoryEmprestimoFuncionario.Add(aluno).Adapt<EmprestimoFuncionarioDto>();
        }

        public EmprestimoFuncionarioDto Update(EmprestimoFuncionarioDto accept)
        {
            var aluno = accept.Adapt<EmprestimoFuncionario>();
            return _repositoryEmprestimoFuncionario.Update(aluno).Adapt<EmprestimoFuncionarioDto>();
        }

        public EmprestimoFuncionario FindByIdQuery(long id)
        {
            return Show().AsNoTracking().AsQueryable()
                .Include(ef => ef.Funcionario)
                .Include(ef => ef.Livro)
                .FirstOrDefault(ef => ef.Id == id);
        }
    }
}
