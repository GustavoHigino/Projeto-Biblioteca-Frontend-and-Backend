using Mapster;
using projetobiblioteca.Data.DTO.User;
using projetobiblioteca.Model;
using projetobiblioteca.Pagination;

namespace projetobiblioteca.Mappers
{
    public static class MapperConfiguringDto
    {
        public static void ConfigureMappings()
        {
            TypeAdapterConfig.GlobalSettings.ForType
                (typeof(PaginationClass<>),
                typeof(PaginationClass<>));

            TypeAdapterConfig<RegisterUser, Users>
                .NewConfig()
                .Map(dest => dest.PasswordHash,
                src => src.Password)
                .Map(dest => dest.Fullname,
                src => src.FullName)
                .Map(dest => dest.Username,
                src => src.Username).TwoWays();

            TypeAdapterConfig<UserDto, Users>
                .NewConfig()
                .Map(dest => dest.Username,
                src => src.Username)
                .Map(dest => dest.PasswordHash,
                src => src.Password).TwoWays();
        }
    }
}
