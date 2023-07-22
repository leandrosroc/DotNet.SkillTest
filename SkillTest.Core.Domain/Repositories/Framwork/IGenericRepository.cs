namespace SkillTest.Core.Domain.Repositories.Framwork
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> Get(int id);
        Task<List<T>> GetAll();
        T Add(T entity);
        void Delete(T entity);
        void Delete(int id);
        T Update(T entity);
    }
}
