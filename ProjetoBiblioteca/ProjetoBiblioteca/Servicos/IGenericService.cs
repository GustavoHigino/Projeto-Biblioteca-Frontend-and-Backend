using projetobiblioteca.Pagination;

namespace projetobiblioteca.Servicos
{
    public interface IGenericService<T,T2>
    {
        Task<PaginationClass<T>> PagedList(int ItensPage, long pageCurrently);
        IQueryable<T> Show();
        T ShowById(long id);
        public T2 Add(T accept, T2 dto);
        public T2 Update(T accept, T2 dto);

    }
}
