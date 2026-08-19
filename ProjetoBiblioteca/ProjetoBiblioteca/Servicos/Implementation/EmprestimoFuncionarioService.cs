using Mapster;
using projetobiblioteca.Data;
using projetobiblioteca.Data.DTO.EmprestimoFuncionario;
using projetobiblioteca.Model;
using projetobiblioteca.Pagination;
using projetobiblioteca.Repositorios;

namespace projetobiblioteca.Servicos.Implementation
{
    public class EmprestimoFuncionarioService :  IEmprestimoFuncionarioService
    {
        private readonly IEmprestimoFuncionarioRepository _repositoryEmprestimoFuncionario;
        public EmprestimoFuncionarioService(IEmprestimoFuncionarioRepository repositoryEmprestimoFuncionario) 
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
            var aluno=accept.Adapt<EmprestimoFuncionario>();
            return _repositoryEmprestimoFuncionario.Add(aluno).Adapt<EmprestimoFuncionarioDto>();
        }

        public EmprestimoFuncionarioDto Update(EmprestimoFuncionarioDto accept)
        {
            var aluno = accept.Adapt<EmprestimoFuncionario>();
            return _repositoryEmprestimoFuncionario.Update(aluno).Adapt<EmprestimoFuncionarioDto>();
        }
    }
}
