using Microsoft.AspNetCore.Mvc.RazorPages;

namespace projetobiblioteca.Pagination
{
    public class PaginationClass<T>
    {
        public PaginationClass( long totalItens,int itensPage,long pageCurrently)
        {
            
            PageCurrently = pageCurrently<1?1:pageCurrently;
            TotalItens = totalItens;
            ItensPage = (itensPage<=0)?10:
                itensPage>MaxPageSize?MaxPageSize:itensPage;
            TotalPage = (long)Math.Ceiling(((decimal)totalItens / (decimal)ItensPage));
            HasNext= PageCurrently < TotalPage;
            HasPreviows= PageCurrently > 1;
            


        }
        
        public static async Task<PaginationClass<T>> CreateAsync
            (long totalItens, int itensPage, long pageCurrently, IQueryable<T> lista)
        {
            var pagination = new PaginationClass<T>(totalItens, itensPage, pageCurrently);
            pagination.Lista = await PageCalculator<T>.Calculo(lista, pagination.PageCurrently, pagination.ItensPage);
            return pagination;
        }
        
        private const int MaxPageSize = 50;
        public List<T> Lista { get; set; } = new();
        public long TotalItens { get; set; }
        public long PageCurrently { get; set; }
        public int ItensPage { get; set; }
        public long TotalPage { get; set; }
        public bool HasPreviows { get; set; }  
        public bool HasNext { get; set; } 

    }

}
