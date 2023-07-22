using MediatR;

namespace SkillTest.Core.Application.Commands.UserCommands.delete
{
    public class DeleteUserCommand : IRequest
    {
        public int Id { get; set; }
    }
}
