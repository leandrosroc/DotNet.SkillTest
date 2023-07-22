using AutoMapper;
using MediatR;
using SkillTest.Core.Application.DTOs;
using SkillTest.Core.Domain.Entity;
using SkillTest.Core.Domain.Repositories.Framwork;

namespace SkillTest.Core.Application.Queries.UserQueries
{
    public class GetByIdUsersQueryHandler : IRequestHandler<GetByIdUsersQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByIdUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<UserDto> Handle(GetByIdUsersQuery request, CancellationToken cancellationToken)
        {
            User user = await _unitOfWork._userRepository.GetById(request.Id);
            UserDto userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}
