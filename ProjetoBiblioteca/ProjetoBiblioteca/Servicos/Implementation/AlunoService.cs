using Mapster;
using Microsoft.EntityFrameworkCore;
using projetobiblioteca.Data;
using projetobiblioteca.Data.DTO.Aluno;
using projetobiblioteca.Model;
using projetobiblioteca.Pagination;
using projetobiblioteca.Repositorios;

namespace projetobiblioteca.Servicos.Implementation
{
    public class AlunoService : IAlunoService

    {
        private readonly IAlunoRepository _alunoRepository;
        public AlunoService(IAlunoRepository alunoRepository
            
        )
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

        public Task<PaginationClass<Aluno>> PagedList(int ItensPage, long pageCurrently)
        {
            var query = _alunoRepository.Show().Include(a => a.EmprestimosAluno
                ).ThenInclude(a => a.Livro);
            return _alunoRepository.PagedList(ItensPage, pageCurrently,query);
        }

        public IQueryable<Aluno> Show()
        {
            return _alunoRepository.Show();
        }

        public Aluno ShowById(long id)
        {
            return _alunoRepository.ShowById(id);
        }

        public AlunoDto Add(AlunoDto accept)
        {
            var aluno = accept.Adapt<Aluno>();
            return _alunoRepository.Add(aluno).Adapt<AlunoDto>() ;

        }

        public AlunoDto Update(AlunoDto accept)
        {
            var aluno=accept.Adapt<Aluno>();
            return _alunoRepository.Update(aluno).Adapt<AlunoDto>();
        }
    }
}
