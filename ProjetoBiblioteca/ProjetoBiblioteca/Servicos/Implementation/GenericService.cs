using Mapster;
using projetobiblioteca.Model.Base;
using projetobiblioteca.Pagination;
using projetobiblioteca.Repositorios;

namespace projetobiblioteca.Servicos.Implementation
{
    public class GenericService<T,T2> : IGenericService<T> where T : ModeloBase
    {
        private readonly IGenericRepository<T> _repositoryGeneric;
        public GenericService(IGenericRepository<T> repositoryGeneric)
        {
            _repositoryGeneric=repositoryGeneric;
        }
        public T2 Add(T accept,T2 dto)
        {//registerUsers
            var entityAccept = dto.Adapt<T>();
            var newEntityDto=_repositoryGeneric.Add(entityAccept);
            
            return newEntityDto.Adapt<T2>();
        }

        public Task<PaginationClass<T>> PagedList(int ItensPage, long pageCurrently)
        {
            return _repositoryGeneric.PagedList(ItensPage, pageCurrently);
        }

        public IQueryable<T> Show()
        {
            return _repositoryGeneric.Show();
        }

        public T ShowById(long id)
        {
            return _repositoryGeneric.ShowById(id);
        }

        public T2 Update(T accept,T2 dto)
        {
            var entity=dto.Adapt<T>();
            var entityUpdated=_repositoryGeneric.Update(entity);
            return entityUpdated.Adapt<T2>();
        }
    }
}
