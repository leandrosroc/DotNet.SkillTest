using SkillTest.Core.Repositories;

namespace SkillTest.Core.Domain.Repositories.Framwork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository _userRepository { get; }
        int Save();
        Task<int> SaveAsync(CancellationToken cancellationToken);
        public void Detach(object entry);
        public void Attach(object entry);
        public void Modif(object entry);

    }
}
