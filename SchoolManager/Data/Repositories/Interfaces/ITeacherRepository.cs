using SchoolManager.Models.Entities;

namespace SchoolManager.Data.Repositories.Interfaces
{
    public interface ITeacherRepository
    {
        Task AddAsync(Teacher teacher, CancellationToken cancellationToken = default);
        Task<List<Teacher>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Teacher?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void Remove(Teacher teacher);
        void Update(Teacher teacher);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
