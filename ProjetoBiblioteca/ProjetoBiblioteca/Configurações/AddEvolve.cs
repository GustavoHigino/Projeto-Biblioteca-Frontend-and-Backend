using EvolveDb;
using Microsoft.Data.SqlClient;
using Serilog;

namespace projetobiblioteca.Configurações
{
    public static class AddEvolve
    {
        public static IServiceCollection
            AddEvolveConfiguration(
            this IServiceCollection service,
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                var connectionString = configuration
                    ["ConnectionMSSQL:ConnectionString"];
                if(string.IsNullOrEmpty(connectionString))
                {
                    throw new ArgumentNullException(
                        "Connection string not" +
                        "found");
                }
                try
                {
                    ExecuteMigration(connectionString);
                }
                catch (Exception ex)
                {

                    Log.Error(ex,
                        "An error ocurred" +
                        " while migrating the database");
                }
            }
            return service;
        }
        public static void ExecuteMigration(
            string connectionString)
        {
            using var evolveConnection = new
                SqlConnection(connectionString);
            var evolve = new Evolve(
                evolveConnection,
                msg => Log.Information(
                    msg))
            {
                Locations = new List<string>
                {
                    "db/migrations",
                    "db/dataset"

                },
                IsEraseDisabled = true
            };
            evolve.Migrate();
        }
    }
}
