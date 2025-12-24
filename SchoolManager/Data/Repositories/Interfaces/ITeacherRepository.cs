using SchoolManager.Models.Entities;

namespace SchoolManager.Data.Repositories.Interfaces
{
    public interface ITeacherRepository
    {
        Task AddAsync(Teacher teacher);
        Task<List<Teacher>> GetAllAsync();
        Task<Teacher?> GetByIdAsync(Guid id);
        Task<bool> Remove(Teacher teacher);
        Task<bool> Update(Teacher teacher);
    }
}
