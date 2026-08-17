using Microsoft.AspNetCore.Mvc.Filters;

namespace projetobiblioteca.HATEOAS.Abstract
{
    public interface IResponseEnricher
    {
        bool CanEnrich(ResultExecutingContext context);
        Task Enrich(ResultExecutingContext context);
    }
}
