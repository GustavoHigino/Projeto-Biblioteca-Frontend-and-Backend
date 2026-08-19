using Mapster;
using Microsoft.EntityFrameworkCore;
using projetobiblioteca.Data;
using projetobiblioteca.Data.DTO.EmprestimoAluno;
using projetobiblioteca.Data.DTO.Livro;
using projetobiblioteca.Model;
using projetobiblioteca.Pagination;
using projetobiblioteca.Repositorios;

namespace projetobiblioteca.Servicos.Implementation
{
    public class EmprestimoAlunoService :  IEmprestimoAlunoService
    {
        private readonly IEmprestimoAlunoRepository _repositoryEmprestimoAluno;
        private readonly ILivroService _livroService;
        public EmprestimoAlunoService(IEmprestimoAlunoRepository repositoryEmprestimoAluno
            , ILivroService livroService) 
        {
            _repositoryEmprestimoAluno = repositoryEmprestimoAluno;
            _livroService= livroService ;
        }
        
        public EmprestimoAlunos Returned(long id)
        {
            var emprestimo = _repositoryEmprestimoAluno.ShowById(id);
            if (emprestimo.Devolvido == true)
            {
                return _repositoryEmprestimoAluno.Returned(id);
            }

            var livro=_livroService.ShowById(emprestimo.IdLivro);
            livro.Emprestados = livro.Emprestados - 1;

            _livroService.Update(livro.Adapt<LivroDto>());
            return _repositoryEmprestimoAluno.Returned(id);

        }

        public EmprestimoAlunos NotReturned(long id)
        {
            var emprestimo = _repositoryEmprestimoAluno.ShowById(id);
            if(emprestimo.Devolvido == false)
            {
                return _repositoryEmprestimoAluno.NotReturned(id);
            }
            var livro = _livroService.ShowById(emprestimo.IdLivro);
            livro.Emprestados = livro.Emprestados + 1;
            _livroService.Update(livro.Adapt<LivroDto>());
            return _repositoryEmprestimoAluno.NotReturned(id);
        }

        public Task<PaginationClass<EmprestimoAlunos>> PagedList(int ItensPage, long pageCurrently)
        {

            var query = Show().AsQueryable().Where(x=>x.Devolvido==false).AsNoTracking();
            return _repositoryEmprestimoAluno.PagedList(ItensPage, pageCurrently,query);
        }

        public IQueryable<EmprestimoAlunos> Show()
        {
            return _repositoryEmprestimoAluno.Show();
        }

        public EmprestimoAlunos ShowById(long id)
        {
            return _repositoryEmprestimoAluno.ShowById(id);
        }

        public EmprestimoAlunoDto Add(EmprestimoAlunoDto accept)
        {
            var emprestimoAluno = accept.Adapt<EmprestimoAlunos>();
            var livro=_livroService.ShowById(accept.IdLivro);
            livro.Emprestados = livro.Emprestados+ 1;
            _livroService.Update(livro.Adapt<LivroDto>());
            return _repositoryEmprestimoAluno.Add(emprestimoAluno).Adapt<EmprestimoAlunoDto>();
            
        }

        public EmprestimoAlunoDto Update(EmprestimoAlunoDto accept)
        {
            var emprestimoAluno=accept.Adapt<EmprestimoAlunos>();
            return _repositoryEmprestimoAluno.Update(emprestimoAluno).Adapt<EmprestimoAlunoDto>();
        }
    }
}
