using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace projetobiblioteca.HATEOAS.Filters
{
    public class HypermediaFilter : ResultFilterAttribute
    {
        private HypermediaFilterOptions _HypermediaOptions;
        public HypermediaFilter(
            HypermediaFilterOptions options)
        {
            _HypermediaOptions = options;
            
        }
        public override void OnResultExecuting(
            ResultExecutingContext context)
        {
            TryEnrichResult(context);
            base.OnResultExecuting(context);
        }
        private void TryEnrichResult(
            ResultExecutingContext context)
        {
            if(context.Result is OkObjectResult
                objectResult)
            {
                var enricher = _HypermediaOptions
                    .ContentResponseEnricherList
                    .FirstOrDefault(options =>
                    options.CanEnrich(context));
                enricher?.Enrich(context).Wait();
            }
        }
    }
}
