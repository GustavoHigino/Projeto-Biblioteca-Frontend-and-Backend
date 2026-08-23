using Mapster;
using projetobiblioteca.Data.DTO.User;
using projetobiblioteca.Model;
using projetobiblioteca.Pagination;

namespace projetobiblioteca.Servicos
{
    public interface IUserService
    {

        public Users FindByUsername(string username);
        public bool RevokeToken(string username);
        TokenDto ValidateCredentials(UserDto userDto);
        TokenDto Refresh(TokenDto tokenDto);
        public Users Add(RegisterUser entity);

        public Task<PaginationClass<UserDto>> PagedList(int ItensPage, long pageCurrently);

        public IQueryable<Users> Show();

        public Users ShowById(long id);

        public Users Update(Users entity);
        public string GenerateEmailConfirmationToken(string username);
        public Users ShowByKey(string key);
        public Users Enable(string key);
    }
}
