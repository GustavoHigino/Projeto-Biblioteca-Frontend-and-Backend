using Mapster;
using projetobiblioteca.Data.DTO.User;
using projetobiblioteca.Model;
using projetobiblioteca.Pagination;
using projetobiblioteca.Repositorios;
using projetobiblioteca.Repositorios.Implementations;
using projetobiblioteca.Tools.Bearer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace projetobiblioteca.Servicos.Implementation
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        public UserService(
            
            IPasswordHasherService passwordHasherService,
            ITokenGenerator tokenGenerator,
            IConfiguration configuration,
            IUserRepository userRepository) 
        {
            _userRepository = userRepository;
            _passwordHasherService = passwordHasherService;
            _tokenGenerator = tokenGenerator;
            _configuration = configuration;
        }
        
        public RegisterUser Add(RegisterUser entity)
        {
            
            var entityAdd =_userRepository.Add
                (entity.Adapt<Users>());
            return entityAdd.Adapt<RegisterUser>();
        }

        public async Task<PaginationClass<UserDto>> PagedList(int ItensPage, long pageCurrently)
        {

            var userPagined= await _userRepository.PagedList(ItensPage, pageCurrently);
            return userPagined.Adapt<PaginationClass<UserDto>>();
        }

        public IQueryable<Users> Show()
        {
            return _userRepository.Show();
        }

        public Users ShowById(long id)
        {
            return _userRepository.ShowById(id);
        }

        public Users Update(Users entity)
        {
            return _userRepository.Update(entity);
        }

        public Users FindByUsername(string username)
        {
            return _userRepository.FindByUserName(username);
        }

        public bool RevokeToken(string username)
        {
            return _userRepository.RevokeToken(username);
        }
        public TokenDto Refresh(TokenDto tokenDto)
        {
            var principal = _tokenGenerator
                .GetPrincipalFromExpiredToken
                (tokenDto.AccessToken);
            var username = principal.Identity.Name;
            var user = FindByUsername(username);
            if (user == null ||
                user.RefreshToken != tokenDto.RefreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }
            return GenerateToken(user);
        }

        public TokenDto ValidateCredentials
            (UserDto userDto)
        {
            var user = FindByUsername(userDto.Username);
            if (user == null)
            {
                return null;
            }
            if (!_passwordHasherService
                .Verify(userDto.PasswordHash
                , user.PasswordHash))
            {
                return null;
            }
            return GenerateToken(user);
        }
        private TokenDto GenerateToken
            (Users user,
            IEnumerable<Claim> existingClaims = null)
        {
            var claims = existingClaims?
                .ToList() ?? new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti
                ,Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames
                .UniqueName,user.Username)
            };
            var accessToken = _tokenGenerator
                .GenerateAccessToken(claims);
            var refreshToken = _tokenGenerator
                .GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow
                .AddDays(
                Convert.ToInt32(_configuration
                ["TokenConfiguration:DaysToExpiry"]));
            Update(user);
            var cretedDate = DateTime.UtcNow;
            var expirationDate = cretedDate
                .AddMinutes(Convert.ToInt32
                (_configuration
                ["TokenConfiguration:Minutes"]));
            return new TokenDto
            {
                Authenticated = true,
                Created = cretedDate.ToString
                ("yyyy-MM-dd HH:mm:ss"),
                Expiration = expirationDate.ToString
                ("yyyy-MM-dd HH:mm:ss"),
                AccessToken = accessToken,
                RefreshToken = refreshToken

            };
        }


    }
}
