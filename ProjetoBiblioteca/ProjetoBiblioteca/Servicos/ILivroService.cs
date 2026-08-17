using projetobiblioteca.Data;
using projetobiblioteca.Model;

namespace projetobiblioteca.Servicos
{
    public interface ILivroService : IGenericService<Livro>
    {
        
        Livro Enable(long id);
        Livro Disable(long id);
    }
}
