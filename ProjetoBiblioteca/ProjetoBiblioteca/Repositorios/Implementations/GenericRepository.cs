using Microsoft.EntityFrameworkCore;
using projetobiblioteca.Context;
using projetobiblioteca.Model.Base;
using projetobiblioteca.Pagination;

namespace projetobiblioteca.Repositorios.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T:ModeloBase
    {
        protected readonly MSSQL _context;
        public GenericRepository(MSSQL context)
        {
            _context= context;
        }
        public IQueryable<T> Show()
        {
            return _context.Set<T>().AsQueryable();
        }

        public T ShowById(long id)
        {
            var finded=_context.Set<T>().Find(id);
            if (finded == null)
            {
                return null;
            }
            return finded;
        }

        public T Add(T entity)
        {
            if(entity==null)
            {
                return null;
            }
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            var entityFinded = ShowById(entity.Id);
            if (entityFinded == null)
            {
                return null;
            }
            _context.Set<T>().Entry(entityFinded)
                .CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return entityFinded;
        }

        public async Task<PaginationClass<T>> PagedList(int itensPage, long pageCurrently,IQueryable<T> query=null)
        {
            query ??= Show();
            var totalItems = await _context.Set<T>().AsNoTracking().CountAsync();
            var paginedList = await PaginationClass<T>.CreateAsync(totalItems,itensPage,pageCurrently,query
                );
           
            return paginedList;
        }
    }
}
