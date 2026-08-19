using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using projetobiblioteca.HATEOAS.Abstract;
using projetobiblioteca.Pagination;

namespace projetobiblioteca.HATEOAS.Utilis
{
    public abstract class ContentResponseEnricher<T>
        : IResponseEnricher where T : ISupportsHypermedia
    {
        public virtual bool CanEnrich(Type contentType)
        {
            return contentType == typeof(T) ||
                contentType == typeof(PaginationClass<T>) ||
                typeof(IEnumerable<T>).IsAssignableFrom(contentType);
        }
        protected abstract Task EnrichModel(
            T content, IUrlHelper urlHelper);
        bool IResponseEnricher.CanEnrich(ResultExecutingContext response)
        {
            if(response.Result is OkObjectResult
                 okObjectResult)
            {
                return CanEnrich(
                    okObjectResult.Value
                    .GetType());
            }
            return false;
        }

        public async Task Enrich(ResultExecutingContext context)
        {

            var urlHelper = new
                UrlHelperFactory().GetUrlHelper
                (context);
            if(context.Result is OkObjectResult
                okObjectResult)
            {
                if(okObjectResult.Value is T model)
                {
                    await EnrichModel(model,
                        urlHelper);
                }
                else if (okObjectResult.Value is
                    IEnumerable<T> collection)
                {
                    foreach(var item in collection)
                    {
                        await EnrichModel(item,
                            urlHelper);
                    }
                }
                else if (okObjectResult.Value is 
                    PaginationClass<T> paginationClass)
                {
                    foreach(var item in paginationClass
                        .Lista)
                    {
                        item.Links?.Clear();
                        await EnrichModel(
                            item, urlHelper);
                    }
                }
            }
            await Task.CompletedTask;
        }
    }
}
