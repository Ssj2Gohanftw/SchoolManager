using SchoolManager.Models.Entities;

namespace SchoolManager.Data.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task AddAsync(Student student, CancellationToken cancellationToken=default);
        Task<List<Student>> GetAllAsync(CancellationToken cancellationToken=default); 
        Task<Student?> GetByIdAsync(Guid id,CancellationToken cancellationToken=default);
        void Remove(Student student);
        void Update(Student student);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
