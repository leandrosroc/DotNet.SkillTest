using AutoMapper;
using MediatR;
using SkillTest.Core.Application.DTOs;
using SkillTest.Core.Domain.Entity;
using SkillTest.Core.Domain.Repositories.Framwork;

namespace SkillTest.Core.Application.Commands.UserCommands.create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User userToAdd = _mapper.Map<User>(request._userDto);
            User addedUser = _unitOfWork._userRepository.Add(userToAdd);

            await _unitOfWork.SaveAsync(cancellationToken);
            return _mapper.Map<UserDto>(addedUser);
        }
    }
}
