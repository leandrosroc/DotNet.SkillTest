using Microsoft.EntityFrameworkCore;
using SkillTest.Core.Domain.Repositories.Framwork;
using SkillTest.Core.Infrastructures.Data;

namespace SkillTest.Core.Infrastructures.Repository.Framwork
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly SkillTestDBContext _context;

        protected GenericRepository(SkillTestDBContext context)
        {
            _context = context;
        }

        public async Task<T?> Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public T Add(T entity)
        {
            return _context.Set<T>().Add(entity).Entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Delete(int id)
        {
            T entity = Get(id).Result;
            _context.Set<T>().Remove(entity);
        }

        public T Update(T entity)
        {
            return _context.Set<T>().Update(entity).Entity;
        }
    }
}
