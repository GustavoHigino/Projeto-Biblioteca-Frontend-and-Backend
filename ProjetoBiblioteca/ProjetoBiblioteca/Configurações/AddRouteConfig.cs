using System.Runtime.CompilerServices;

namespace projetobiblioteca.Configurações
{
    public static class AddRouteConfig
    {
        public static IServiceCollection AddRouteConfiguration
        (this IServiceCollection service)
        {
            service.Configure<RouteOptions>
                (options =>
                {
                    options.LowercaseUrls = true;
                    options.LowercaseQueryStrings = true;
                });
            return service;
        }
    }
}
