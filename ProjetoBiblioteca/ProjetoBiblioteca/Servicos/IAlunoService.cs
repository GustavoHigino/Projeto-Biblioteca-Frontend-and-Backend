using projetobiblioteca.Data;
using projetobiblioteca.Pagination;

namespace projetobiblioteca.Servicos
{
    public interface IAlunoService: IGenericService<Aluno>
    {
        
        Aluno Enable(long id);
        Aluno Disable(long id);
        


    }
}
