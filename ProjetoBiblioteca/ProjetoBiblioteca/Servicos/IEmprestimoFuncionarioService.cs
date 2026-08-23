using projetobiblioteca.Data.DTO.EmprestimoFuncionario;
using projetobiblioteca.Model;
using projetobiblioteca.Pagination;

namespace projetobiblioteca.Servicos
{
    public interface IEmprestimoFuncionarioService 
    {
        Task<PaginationClass<EmprestimoFuncionario>> PagedList(int ItensPage, long pageCurrently);
        IQueryable<EmprestimoFuncionario> Show();
        EmprestimoFuncionario ShowById(long id);
        public EmprestimoFuncionarioDto Add(EmprestimoFuncionarioDto accept);
        public EmprestimoFuncionarioDto Update(EmprestimoFuncionarioDto accept);
        EmprestimoFuncionario Returned(long id);
        EmprestimoFuncionario NotReturned(long id);
        EmprestimoFuncionario FindByIdQuery(long id);
    }
}
