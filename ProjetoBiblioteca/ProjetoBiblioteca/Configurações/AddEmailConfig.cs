using projetobiblioteca.Mail.Data;

namespace projetobiblioteca.Configurações
{
    public class AddEmailConfig
    {
        public static IServiceCollection
            AddEmailConfiguration(
            this IServiceCollection service,
            IConfiguration configuration)
        {
            var section = configuration
                .GetSection("Email");
            var configs = section.Get<EmailData>();
            if (configs == null)
            {
                throw new ArgumentNullException
                    (nameof(configs),
                    "Email configuration is missing" +
                    " or invalid.");
            }
            configs.Username = Environment
                .GetEnvironmentVariable(
                "EMAIL_USERNAME") ??
                configs.Username;
            configs.Password = Environment
                .GetEnvironmentVariable(
                "EMAIL_PASSWORD") ??
                configs.Password;
            service.AddSingleton(configs);
            return service;
        }
    }
}
