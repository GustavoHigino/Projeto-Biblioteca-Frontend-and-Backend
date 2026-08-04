using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using projetobiblioteca.Context;

namespace projetobiblioteca.Configurações
{
    public static class AddContextConfiguration
    {
        public static IServiceCollection AddContextDatabase(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            var connectionString = configuration
                ["ConnectionMSSQL:ConnectionString"];
            if(connectionString == null)
            {
                throw new ArgumentNullException(
                    "A string de conexão do banco" +
                    " de dados é nula");
            }
            var senha = Environment
                .GetEnvironmentVariable
                ("Password_Database");
            if (!string.IsNullOrWhiteSpace(senha))
            {
                connectionString=connectionString.Replace
                    ("${Password_Database}"
                    , senha);
            }
            services.AddDbContext<MSSQL>(
                options =>
                options.UseSqlServer(connectionString));
            return services;

        }
    }
}
