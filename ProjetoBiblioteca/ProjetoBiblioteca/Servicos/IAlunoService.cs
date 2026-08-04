using projetobiblioteca.Data;

namespace projetobiblioteca.Servicos
{
    public interface IAlunoService
    {
        IQueryable<Fundionario> Show();
        Fundionario ShowById(long id);
        Fundionario Add(Fundionario aluno);
        Fundionario Update(Fundionario aluno);
        bool EnableOrDisable(bool enableOrDisable);


    }
}
