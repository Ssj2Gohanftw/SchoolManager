namespace SchoolManager.Data.Repositories.Interfaces
{
    public interface IRepository<T> where T: class
    {
        Task<List<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task<T?> GetByIdAsync(Guid id);
        Task<bool> Remove(T entity);
        Task<bool> Update(T entity);
    }
}
