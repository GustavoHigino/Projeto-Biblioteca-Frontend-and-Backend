using Mapster;
using projetobiblioteca.Data.DTO.User;
using projetobiblioteca.Mappers;
using projetobiblioteca.Model;
using projetobiblioteca.Pagination;

namespace projetobiblioteca.Configuracoes
{
    public static class AddMappingConfiguration
    {
        public static IServiceCollection AddMappingConfig(
            this IServiceCollection services)
        {
            MapperConfiguringDto.ConfigureMappings();
            services.AddMapster();
            return services;
        }
        

    }
}
