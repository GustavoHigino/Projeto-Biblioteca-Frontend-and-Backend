using projetobiblioteca.Data;
using projetobiblioteca.Data.DTO.Aluno;
using projetobiblioteca.Model;
using projetobiblioteca.Pagination;

namespace projetobiblioteca.Servicos
{
    public interface IAlunoService
    {
        Task<PaginationClass<Aluno>> PagedList
            (int ItensPage, long pageCurrently);
        IQueryable<Aluno> Show();
        Aluno ShowById(long id);
        public AlunoDto Add(AlunoDto accept);
        public AlunoDto Update(AlunoDto accept);
        Aluno Enable(long id);
        Aluno Disable(long id);
        Aluno FindByIdQuery(long id);
        


    }
}
