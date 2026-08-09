using projetobiblioteca.Model;

namespace projetobiblioteca.Servicos
{
    public interface IEmprestimoFuncionarioService : IGenericService<EmprestimoFuncionario>
    {
        
        EmprestimoFuncionario Returned(long id);
        EmprestimoFuncionario NotReturned(long id);
    }
}
