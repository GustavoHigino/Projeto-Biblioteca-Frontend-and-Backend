namespace projetobiblioteca.Configuracoes
{
    public static class AddCorsConfiguration
    {
        private static string[] GetAllowedOrigins
            (IConfiguration configuration)
        {
            return configuration.GetSection
                ("Cors:Origins")
                .Get<string[]>()?? Array.Empty<string> ();
        }
        public static void AddCorsConfig(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var origins = GetAllowedOrigins
                (configuration);
            services.AddCors(options =>
            {
                options.AddPolicy("LocalPolicy",
                    policy =>
                    policy.WithOrigins(origins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
                options.AddPolicy("AnyPolicy",
                    policy =>
                    policy.SetIsOriginAllowed(
                        origin => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());

            }
            );
            
        }
        public static IApplicationBuilder UseCorsConfiguration(
            this IApplicationBuilder app)
        {
            
            app.UseCors("LocalPolicy");
            return app;
        }
    }
}
