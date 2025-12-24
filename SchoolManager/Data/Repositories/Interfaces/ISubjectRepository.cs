using SchoolManager.Models.Entities;

namespace SchoolManager.Data.Repositories.Interfaces
{
    public interface ISubjectRepository
    {
        Task AddAsync(Subject subject);
        Task<List<Subject>> GetAllAsync();
        Task<Subject?> GetByIdAsync(Guid id);
        Task<bool> Remove(Subject subject);
        Task<bool> Update(Subject subject);

    }
}
