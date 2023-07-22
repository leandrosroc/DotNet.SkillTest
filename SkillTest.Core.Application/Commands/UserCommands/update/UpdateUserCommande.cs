using MediatR;
using SkillTest.Core.Application.DTOs;

namespace SkillTest.Core.Application.Commands.UserCommands.update
{
    public class UpdateUserCommande : IRequest<UserDto>
    {
        public UserDto _userDto { get; set; }
    }
}
