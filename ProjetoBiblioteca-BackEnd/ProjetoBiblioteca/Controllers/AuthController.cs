using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projetobiblioteca.Data.DTO.User;
using projetobiblioteca.Mail.Dto;
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
        private readonly ILogger<AuthController> _logger;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        public AuthController(
            IUserService userService,
            IEmailService emailService,
            IPasswordHasherService passwordHasher,
            ILogger<AuthController> logger,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
            _passwordHasher = passwordHasher;
            _userService = userService;
            _emailService = emailService;
        }
        [HttpPost("signin")]
        [AllowAnonymous]
        public IActionResult SignUp(UserDto user)
        {
            var userFound=_userService.FindByUsername(user.Username);
            if(userFound.Key!= "verificado")
            {
                return Unauthorized("Verify your email");
            }
            var tokenDto=_userService.ValidateCredentials(user);
            if (tokenDto == null)
            {
                return Unauthorized("Invalid Credentials");
            }
            Response.Cookies.Append("X-Access-Token",
                tokenDto.AccessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite=SameSiteMode.Strict,
                    Expires=DateTime.UtcNow.AddMinutes
                    (Convert.ToInt32
                    (_configuration
                    ["TokenConfiguration:Minutes"]))
                });
            Response.Cookies.Append("X-Refresh-Token",
                tokenDto.RefreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays
                    (Convert.ToInt32
                    (_configuration
                    ["TokenConfiguration:DaysToExpiry"]))
                });
            return Ok("login done with successfully");
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register(RegisterUser user)
        {
            
            var userWithHash = user with
            {PasswordHash=
                _passwordHasher
                .Hash(user.PasswordHash)
            };
            var registerUser =
                _userService.Add(userWithHash);

            var emailRequest = new EmailRequestDTO(user.Email,
                "Confirmação do login",
                $"<a href=\"https://localhost:7082/Auth/Enable/{registerUser.Key}\">Clique aqui para validar a conta</a>");

            _emailService.SendSimpleEmail(emailRequest);
            return Ok(registerUser.Adapt<RegisterUser>());
            
        }
        [HttpPatch("revoketoken")]
        [Authorize]
        public IActionResult RevokeToken(long id)
        {
            var username = User.Identity.Name;
            var value=_userService.RevokeToken(username);
            Response.Cookies.Delete("X-Access-Token");
            Response.Cookies.Delete("X-Refresh-Token");
            return Ok(value);
        }
        [AllowAnonymous]
        [HttpPut("refresh")]
        public IActionResult refresh(TokenDto tokenDto)
        {
            if(!Request.Cookies.TryGetValue
                ("X-Refresh-Token",
                out var refreshTok))
            {
                return BadRequest("Refresh token " +
                    "not found on cookies");
            }
            if(tokenDto == null)
            {
                return BadRequest();
            }
            var refreshToken= _userService.Refresh(
                new TokenDto
                {
                    RefreshToken= refreshTok
                });
            if(refreshToken == null)
            {
                return BadRequest();
            }
            Response.Cookies.Append("X-Access-Token",
                refreshToken.AccessToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddMinutes
                    (Convert.ToInt32
                    (_configuration
                    ["TokenConfiguration:Minutes"]))
                });
            Response.Cookies.Append("X-Refresh-Token",
                refreshToken.RefreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays
                    (Convert.ToInt32
                    (_configuration
                    ["TokenConfiguration:DaysToExpiry"]))
                });
            return Ok("token renovado com sucesso.");
        }
        [AllowAnonymous]
        [HttpGet("Enable/{key}")]
        public IActionResult Enable(string key)
        {
            var user=_userService.Enable(key);
            
            return Ok(user);
        }
        [AllowAnonymous]
        [HttpPatch("Disable/{id:long}")]
        public IActionResult Disable(long id)
        {
            var user=_userService.ShowById(id);
            if (user == null)
            {
                return NotFound();
            }
            if (user.Enable == false)
            {
                return BadRequest("User already false");
            }
            user.Enable=false;
            _userService.Update(user);
            return Ok(user.Adapt<RegisterUser>());
        }


    }
}
