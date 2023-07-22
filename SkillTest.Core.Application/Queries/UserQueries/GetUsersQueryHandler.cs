using AutoMapper;
using MediatR;
using SkillTest.Core.Application.DTOs;
using SkillTest.Core.Domain.Entity;
using SkillTest.Core.Domain.Repositories.Framwork;

namespace SkillTest.Core.Application.Queries.UserQueries
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            List<User> users = new();
            List<UserDto> usersDto = new();
            switch (request.PropertyName)
            {
                case UsersCrieriaEnum.All:
                    {
                        users = await _unitOfWork._userRepository.GetAll();
                        break;
                    }
                case UsersCrieriaEnum.ByEmail:
                    {
                        users = await _unitOfWork._userRepository.GetUserByEmail((string)request.PropertyValue);
                        break;
                    }
                case UsersCrieriaEnum.ByRefreshToken:
                    {
                        users = await _unitOfWork._userRepository.GetUserByRefreshToken((string)request.PropertyValue);
                        break;
                    }
                default: break;

            }

            usersDto = users.Select(u => _mapper.Map<UserDto>(u)).ToList();
            return usersDto;
        }
    }
    public enum UsersCrieriaEnum
    {
        All,
        ByCompany,
        ByEmail,
        ByFirstName,
        ByLastName,
        ByRefreshToken
    }
}
