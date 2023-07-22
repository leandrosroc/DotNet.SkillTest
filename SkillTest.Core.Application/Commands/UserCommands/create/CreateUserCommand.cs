using MediatR;
using SkillTest.Core.Application.DTOs;

namespace SkillTest.Core.Application.Commands.UserCommands.create
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public UserDto _userDto { get; set; }
    }
}
