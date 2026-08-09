using projetobiblioteca.Data;

namespace projetobiblioteca.Servicos
{
    public interface IFuncionarioService : IGenericService<Funcionario>
    {
        
        Funcionario Enable(long id);
        Funcionario Disable(long id);
         
    }
}
