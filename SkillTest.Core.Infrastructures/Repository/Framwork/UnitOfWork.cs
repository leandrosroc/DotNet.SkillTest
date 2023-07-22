using Microsoft.EntityFrameworkCore;
using SkillTest.Core.Domain.Repositories.Framwork;
using SkillTest.Core.Infrastructures.Data;
using SkillTest.Core.Repositories;

namespace SkillTest.Core.Infrastructures.Repository.Framwork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SkillTestDBContext _context;
        public IUserRepository _userRepository { get; }


        public UnitOfWork(SkillTestDBContext skillTestDBContext, IUserRepository userRepository)
        {
            _context = skillTestDBContext;
            _userRepository = userRepository;

        }

        public void Detach(object entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        public void Attach(object entity)
        {
            _context.Entry(entity).State = EntityState.Unchanged;
        }

        public void Modif(object entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
