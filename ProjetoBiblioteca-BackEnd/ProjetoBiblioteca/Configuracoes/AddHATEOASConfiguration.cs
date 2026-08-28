using projetobiblioteca.HATEOAS.Enricher;
using projetobiblioteca.HATEOAS.Filters;

namespace projetobiblioteca.Configuracoes
{
    public static class AddHATEOASConfiguration
    {
        public static IServiceCollection
            AddHATEOASConfig(
            this IServiceCollection services)
        {
            var filterOptions = new
                HypermediaFilterOptions();
            filterOptions.ContentResponseEnricherList
                .Add(new AlunoEnricher());
            services.AddSingleton
                (filterOptions);
            services.AddScoped<HypermediaFilter>();
            return services;
        }
        public static void UseHATEOASRoutes(
            this IEndpointRouteBuilder app)
        {
            app.MapControllerRoute(
                "Default",
                "{controller=values}/v1/{id?}");
        }
    }
}
