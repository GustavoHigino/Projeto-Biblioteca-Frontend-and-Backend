using Serilog;

namespace projetobiblioteca.Configurações
{
    public static class AddLogging
    {
        public static void AddLoggingSerilog(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Debug()
                .CreateLogger();
            builder.Host.UseSerilog();

        }
    }
}
