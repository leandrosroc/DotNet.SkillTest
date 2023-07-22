using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SkillTest.Core.Application.Commands.UserCommands.create;
using SkillTest.Core.Application.Commands.UserCommands.delete;
using SkillTest.Core.Application.Commands.UserCommands.update;
using SkillTest.Core.Application.DTOs;
using SkillTest.Core.Application.Enums;
using SkillTest.Core.Application.Queries.UserQueries;
using SkillTest.Core.Application.Services;

namespace SkillTest.API.UI.Controllers.User
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : AbstractContoller
    {

        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;
        private readonly IAuthService _authService;

        public UserController(ILogger<UserController> logger, IMediator mediator, IAuthService authService)
        {
            _logger = logger;
            _mediator = mediator;
            _authService = authService;
        }

        [Route("")]
        [HttpGet, Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> GetAll()
        {
            string role = GetClaim(ClaimTypes.Role);
            var results = new List<UserDto>();
            if (RoleEnum.Admin.ToString().Equals(role))
            {
                results = await _mediator.Send(new GetUsersQuery() { PropertyName = UsersCrieriaEnum.All });
            }

            return Ok(results);
        }

        [Route("{id:int}")]
        [HttpGet, Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> GetById(int id)
        {
            UserDto result = await _mediator.Send(new GetByIdUsersQuery() { Id = id });
            if (result == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "Usuário não encontrado");
            }

            return Ok(result);
        }

        [Route("")]
        [HttpPost, Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Create(RegisterDto registerDto)
        {
            _authService.CreatePasswordHash(registerDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            UserDto userDto = new()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = registerDto.Role
            };

            UserDto result = await _mediator.Send(new CreateUserCommand() { _userDto = userDto });
            return Ok(result);
        }

        [Route("{id:int}")]
        [HttpPut, Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Edit(int id, UserDto userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest("ID diferente entre solicitação e corpo");
            }

            string role = GetClaim(ClaimTypes.Role);

            if (!RoleEnum.Admin.ToString().Equals(role))
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Você não tem permissão de atualizar este usuário");
            }

            UserDto result = await _mediator.Send(new UpdateUserCommande() { _userDto = userDto });
            return Ok(result);

        }

        [HttpDelete, Authorize(Roles = "Admin, SuperAdmin")]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            string role = GetClaim(ClaimTypes.Role);
            UserDto existingUserDto = await _mediator.Send(new GetByIdUsersQuery() { Id = id });
            if (existingUserDto == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Usuário não encontrado");
            }
            if (!RoleEnum.Admin.ToString().Equals(role))
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Você não tem permissão de apagar este usuário");
            }

            await _mediator.Send(new DeleteUserCommand() { Id = id });
            return Ok();
        }
    }
}