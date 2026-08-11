using projetobiblioteca.Data.DTO;
using projetobiblioteca.Model;

namespace projetobiblioteca.Servicos
{
    public interface IUserService:IGenericService<Users>
    {
        
        public Users FindByUsername(string username);
        public bool RevokeToken(string username);
        TokenDto ValidateCredentials(UserDto userDto);
        TokenDto Refresh(TokenDto tokenDto);
    }
}
