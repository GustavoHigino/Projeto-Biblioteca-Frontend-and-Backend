using projetobiblioteca.Data;
using projetobiblioteca.Model;

namespace projetobiblioteca.Servicos
{
    public interface IFuncionarioService : IGenericService<Funcionario>
    {
        
        Funcionario Enable(long id);
        Funcionario Disable(long id);
         
    }
}
