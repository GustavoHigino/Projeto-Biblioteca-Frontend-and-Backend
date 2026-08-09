using projetobiblioteca.Pagination;

namespace projetobiblioteca.Servicos
{
    public interface IGenericService<T>
    {
        Task<PaginationClass<T>> PagedList(int ItensPage, long pageCurrently);
        IQueryable<T> Show();
        T ShowById(long id);
        T Add(T entity);
        T Update(T entity);

    }
}
