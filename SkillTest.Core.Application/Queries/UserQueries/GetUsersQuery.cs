using MediatR;
using SkillTest.Core.Application.DTOs;

namespace SkillTest.Core.Application.Queries.UserQueries
{
    public class GetUsersQuery : IRequest<List<UserDto>>
    {
        public UsersCrieriaEnum PropertyName { get; set; }
        public Object PropertyValue { get; set; }
    }


}
