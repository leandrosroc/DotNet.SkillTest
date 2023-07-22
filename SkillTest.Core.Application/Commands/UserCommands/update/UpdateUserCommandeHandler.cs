using AutoMapper;
using MediatR;
using SkillTest.Core.Application.DTOs;
using SkillTest.Core.Domain.Entity;
using SkillTest.Core.Domain.Repositories.Framwork;

namespace SkillTest.Core.Application.Commands.UserCommands.update
{
    public class UpdateUserCommandeHandler : IRequestHandler<UpdateUserCommande, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateUserCommandeHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(UpdateUserCommande request, CancellationToken cancellationToken)
        {
            User dUserToUpdate = await _unitOfWork._userRepository.Get(request._userDto.Id);
            dUserToUpdate.FirstName = request._userDto.FirstName;
            dUserToUpdate.LastName = request._userDto.LastName;
            dUserToUpdate.Email = request._userDto.Email;
            dUserToUpdate.Role = request._userDto.Role;

            User updatedUser = _unitOfWork._userRepository.Update(dUserToUpdate);
            await _unitOfWork.SaveAsync(cancellationToken);

            return _mapper.Map<UserDto>(updatedUser);
        }
    }
}

