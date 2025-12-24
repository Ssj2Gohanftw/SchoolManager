using SchoolManager.Models.Entities;

namespace SchoolManager.Data.Repositories.Interfaces
{
    public interface IStudentSubjectRepository
    {
        Task AddAsync(StudentSubject studentSubject);
        Task<bool> ExistsAsync(Guid studentId, Guid subjectId);
        Task<List<Subject>> GetSubjectsForStudentAsync(Guid studentId);
        Task<bool> RemoveAsync(Guid studentId, Guid subjectId);
    }
}
