using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projetobiblioteca.Data.DTO;
using projetobiblioteca.Model;
using projetobiblioteca.Servicos;
using projetobiblioteca.Tools.Bearer;

namespace projetobiblioteca.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasherService _passwordHasher;
        public AuthController(
            IUserService userService,
            ITokenGenerator tokenGenerator,
            IPasswordHasherService passwordHasher)
        {
            _passwordHasher = passwordHasher;
            _userService = userService;
        }
        [HttpPost("signin")]
        [AllowAnonymous]
        public IActionResult SignUp(UserDto user)
        {
            var token=_userService.ValidateCredentials(user);
            return Ok(token);
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register(Users user)
        {
            user.PasswordHash = _passwordHasher
                .Hash(user.PasswordHash);
            var registerUser = _userService.Add(user);
            return Ok(registerUser);
            
        }
        [HttpPatch("revoketoken")]
        [Authorize]
        public IActionResult RevokeToken(long id)
        {
            var username = User.Identity.Name;
            var value=_userService.RevokeToken(username);
            return Ok(value);
        }
        [AllowAnonymous]
        [HttpPut("refresh")]
        public IActionResult refresh(TokenDto tokenDto)
        {
            if(tokenDto == null)
            {
                return BadRequest();
            }
            var refreshToken= _userService.Refresh(tokenDto);
            if(refreshToken == null)
            {
                return BadRequest();
            }
            return Ok(refreshToken);
        }

    }
}
