using Microsoft.EntityFrameworkCore;

namespace projetobiblioteca.Pagination
{
    public static class PageCalculator<T>
    {
        public  static List<T> Calculo(IQueryable<T> lista,
            long pageCurrently , int itensPage)
        {

            var listaDone =  lista.Skip((int)(pageCurrently - 1) * itensPage)
                .Take(itensPage).ToList();
            return listaDone;
        }

    }
}
