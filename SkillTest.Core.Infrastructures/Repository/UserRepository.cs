using Microsoft.EntityFrameworkCore;
using SkillTest.Core.Domain.Entity;
using SkillTest.Core.Infrastructures.Data;
using SkillTest.Core.Infrastructures.Repository.Framwork;
using SkillTest.Core.Repositories;

namespace SkillTest.Core.Infrastructures.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(SkillTestDBContext context) : base(context)
        {

        }

        public Task<List<User>> GetUserByEmail(string email)
        {
            IQueryable<User> user = _context.Users.Where(x => x.Email == email).AsNoTracking();
            return user.ToListAsync();
        }

        public Task<List<User>> GetUserByRefreshToken(string refreshToken)
        {
            IQueryable<User> user = _context.Users.Where(x => x.RefreshToken == refreshToken).AsNoTracking();

            return user.ToListAsync();
        }

        public Task<User?> GetById(int id)
        {
            IQueryable<User> user = _context.Users.Where(x => x.Id == id);
            return user.FirstOrDefaultAsync();
        }
    }
}
