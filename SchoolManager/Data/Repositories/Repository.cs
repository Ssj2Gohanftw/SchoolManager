using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;

namespace SchoolManager.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly DbSet<T> _entity;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _entity = _dbContext.Set<T>();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _entity.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        public virtual async Task AddRangeAsync(List<T> entities)
        {
            await _entity.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _entity.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _entity.FindAsync(id);
        }

        public virtual async Task<bool> Remove(T entity)
        {
            _entity.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> Update(T entity)
        {
            _entity.Update(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }

}
