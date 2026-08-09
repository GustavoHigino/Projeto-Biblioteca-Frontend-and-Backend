using projetobiblioteca.Model.Base;
using projetobiblioteca.Pagination;
using projetobiblioteca.Repositorios;

namespace projetobiblioteca.Servicos.Implementation
{
    public class GenericService<T> : IGenericService<T> where T : ModeloBase
    {
        private readonly IGenericRepository<T> _repositoryGeneric;
        public GenericService(IGenericRepository<T> repositoryGeneric)
        {
            _repositoryGeneric=repositoryGeneric;
        }
        public T Add(T entity)
        {
            return _repositoryGeneric.Add(entity);
        }

        public Task<PaginationClass<T>> PagedList(int ItensPage, long pageCurrently)
        {
            return PagedList(ItensPage, pageCurrently);
        }

        public IQueryable<T> Show()
        {
            return Show();
        }

        public T ShowById(long id)
        {
            return ShowById(id);
        }

        public T Update(T entity)
        {
            return Update(entity);
        }
    }
}
