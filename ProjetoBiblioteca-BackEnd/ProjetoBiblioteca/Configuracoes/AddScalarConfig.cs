using Scalar.AspNetCore;

namespace projetobiblioteca.Configuracoes
{
    public static class AddScalarConfig
    {
        public static WebApplication UseScalarConfiguration(
            this WebApplication app)
        {
            app.MapScalarApiReference(
                "/scalar",
                options =>
                {
                    options.WithTitle("Asp.Net 10 library project")
                    .WithOpenApiRoutePattern("/openapi/v1.json");
                });
            return app;
        }
    }
}
