using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SkillTest.Core.Application.Auth;
using SkillTest.Core.Application.DTOs;
using SkillTest.Core.Application.Commands.UserCommands.create;
using SkillTest.Core.Application.Commands.UserCommands.update;
using SkillTest.Core.Application.Queries.UserQueries;
using SkillTest.Core.Application.Services;

namespace SkillTest.API.UI.Controllers.Auth
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IMediator _mediator;
        private readonly IAuthService _authService;


        public AuthController(ILogger<AuthController> logger, IMediator mediator, IAuthService authService)
        {
            _logger = logger;
            _mediator = mediator;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterDto>> Register(RegisterDto registerDto)
        {
            _authService.CreatePasswordHash(registerDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            UserDto userDto = new()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = "Admin",
            };

            UserDto result = await _mediator.Send(new CreateUserCommand { _userDto = userDto });
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto loginDto)
        {
            List<UserDto> results = await _mediator.Send(new GetUsersQuery() { PropertyName = UsersCrieriaEnum.ByEmail, PropertyValue = loginDto.Email });
            if (results.Count == 0)
            {
                return BadRequest("Usuário não encontrado.");
            }
            if (results.Count > 1)
            {
                return BadRequest("Erro: Muitos usuários encontrados.");
            }
            UserDto userDto = results[0];
            if (!_authService.VerifyPasswordHash(loginDto.Password, userDto.PasswordHash, userDto.PasswordSalt))
            {
                return BadRequest("Senha incorreta.");
            }

            // obter usuário por e-mail e recuperar a função no banco de dados.
            string token = _authService.CreateToken(userDto, userDto.Role);

            var refreshToken = _authService.GenerateRefreshToken();
            await SetRefreshTokenAsync(userDto, refreshToken);

            return Ok(token);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            List<UserDto> results = await _mediator.Send(new GetUsersQuery() { PropertyName = UsersCrieriaEnum.ByRefreshToken, PropertyValue = refreshToken });
            if (results.Count != 1)
            {
                return Unauthorized("Invalid Refresh Token.");
            }

            UserDto userDto = results[0];
            if (userDto.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }

            string token = _authService.CreateToken(userDto, userDto.Role);
            var newRefreshToken = _authService.GenerateRefreshToken();
            await SetRefreshTokenAsync(userDto, newRefreshToken);

            return Ok(token);
        }

        private async Task SetRefreshTokenAsync(UserDto userDto, RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            userDto.RefreshToken = newRefreshToken.Token;
            userDto.TokenCreated = newRefreshToken.Created;
            userDto.TokenExpires = newRefreshToken.Expires;

            UserDto result = await _mediator.Send(new UpdateUserCommande() { _userDto = userDto });
        }
    }
}
