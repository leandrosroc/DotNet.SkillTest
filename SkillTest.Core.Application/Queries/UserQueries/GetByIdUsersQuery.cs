using MediatR;
using SkillTest.Core.Application.DTOs;

namespace SkillTest.Core.Application.Queries.UserQueries
{
    public class GetByIdUsersQuery : IRequest<UserDto>
    {
        public int Id { get; set; }

    }
}
