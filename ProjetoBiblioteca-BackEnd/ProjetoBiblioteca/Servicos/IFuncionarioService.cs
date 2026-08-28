using projetobiblioteca.Data;
using projetobiblioteca.Data.DTO.Funcionario;
using projetobiblioteca.Model;
using projetobiblioteca.Pagination;

namespace projetobiblioteca.Servicos
{
    public interface IFuncionarioService 
    {
        Task<PaginationClass<Funcionario>> PagedList(int ItensPage, long pageCurrently);
        IQueryable<Funcionario> Show();
        Funcionario ShowById(long id);
        public FuncionarioDto Add(FuncionarioDto accept);
        public FuncionarioDto Update(FuncionarioDto accept);
        Funcionario Enable(long id);
        Funcionario Disable(long id);
        Funcionario FindByIdQuery(long id);

    }
}
