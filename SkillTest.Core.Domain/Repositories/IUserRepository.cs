using SkillTest.Core.Domain.Entity;
using SkillTest.Core.Domain.Repositories.Framwork;

namespace SkillTest.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<List<User>> GetUserByEmail(string email);
        public Task<List<User>> GetUserByRefreshToken(string property);
        public Task<User?> GetById(int id);
    }
}
