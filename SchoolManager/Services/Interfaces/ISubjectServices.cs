using SchoolManager.Models.Dtos.Subject;
using SchoolManager.Models.Entities;

namespace SchoolManager.Services.Interfaces
{
    public interface ISubjectServices
    {
        Task<List<Subject>> GetAllAsync();
        Task<Subject?> GetSubjectByIdAsync(Guid id);
        Task<Subject> AddSubjectAsync(AddSubjectDto addSubjectDto);
        Task<bool> UpdateSubjectAsync(Guid id, UpdateSubjectDto updateStudentDto);
        Task<bool> DeleteSubjectAsync(Guid id);
    }
}
