using projetobiblioteca.Data.DTO.EmprestimoAluno;
using projetobiblioteca.Model;
using projetobiblioteca.Pagination;

namespace projetobiblioteca.Servicos
{
    public interface IEmprestimoAlunoService 
    {
        Task<PaginationClass<EmprestimoAlunos>> PagedList(int ItensPage, long pageCurrently);
        IQueryable<EmprestimoAlunos> Show();
        EmprestimoAlunos ShowById(long id);
        public EmprestimoAlunoDto Add(EmprestimoAlunoDto accept);
        public EmprestimoAlunoDto Update(EmprestimoAlunoDto accept);

        EmprestimoAlunos Returned(long id);
        EmprestimoAlunos NotReturned(long id);
    }
}
