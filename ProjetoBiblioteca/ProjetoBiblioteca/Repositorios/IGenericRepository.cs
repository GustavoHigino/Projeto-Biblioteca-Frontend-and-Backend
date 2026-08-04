using projetobiblioteca.Data;
using projetobiblioteca.Model.Base;

namespace projetobiblioteca.Repositorios
{
    public interface IGenericRepository<T> where T :ModeloBase  
    {
        IQueryable<T> Show();
        T ShowById(long id);
        T Add(T entity);
        T Update(T entity);
        

    }
}
