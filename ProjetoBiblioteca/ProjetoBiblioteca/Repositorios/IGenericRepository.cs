using projetobiblioteca.Data;
using projetobiblioteca.Model.Base;
using projetobiblioteca.Pagination;

namespace projetobiblioteca.Repositorios
{
    public interface IGenericRepository<T> where T :ModeloBase  
    {
        Task<PaginationClass<T>> PagedList(int itensPage, long pageCurrently);
        IQueryable<T> Show();
        T ShowById(long id);
        T Add(T entity);
        T Update(T entity);
        

    }
}
