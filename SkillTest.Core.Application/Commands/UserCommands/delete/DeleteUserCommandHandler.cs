using AutoMapper;
using MediatR;
using SkillTest.Core.Domain.Repositories.Framwork;

namespace SkillTest.Core.Application.Commands.UserCommands.delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork._userRepository.Delete(request.Id);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
