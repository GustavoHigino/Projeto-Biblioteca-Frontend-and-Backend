using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;

namespace projetobiblioteca.Configurações
{
    public static class AddBearerConfiguration
    {
        public static IServiceCollection AddBearerConfig(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(
                options =>
                {
                    options.TokenValidationParameters
                    = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration
                        ["TokenConfiguration:Issuer"],
                        ValidAudience = configuration
                        ["TokenConfiguration:Audience"],
                        IssuerSigningKey = new
                        SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes
                            (configuration
                            ["TokenConfiguration:Secret"]))
                    };
                });
            return services;
        }
        

        
    }
}
