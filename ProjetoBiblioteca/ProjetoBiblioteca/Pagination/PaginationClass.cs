using Microsoft.AspNetCore.Mvc.RazorPages;

namespace projetobiblioteca.Pagination
{
    public class PaginationClass<T>
    {
        public PaginationClass( long totalItens,int itensPage,long pageCurrently,IQueryable<T> lista)
        {
            
            PageCurrently = pageCurrently<1?1:pageCurrently;
            TotalItens = totalItens;
            ItensPage = (itensPage<=0)?10:
                itensPage>MaxPageSize?MaxPageSize:itensPage;
            TotalPage = (long)Math.Ceiling(((decimal)totalItens / (decimal)ItensPage));
            HasNext= PageCurrently < TotalPage;
            HasPreviows= PageCurrently > 1;
            Lista = PageCalculator<T>.Calculo(lista,
                PageCurrently, ItensPage);


        }
        private const int MaxPageSize = 50;
        public List<T> Lista { get; set; }
        public long TotalItens { get; set; }
        public long PageCurrently { get; set; }
        public int ItensPage { get; set; }
        public long TotalPage { get; set; }
        public bool HasPreviows { get; set; }  
        public bool HasNext { get; set; } 

    }

}
