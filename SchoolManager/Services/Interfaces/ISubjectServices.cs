using SchoolManager.Models.Dtos.Subject;
using SchoolManager.Models.Entities;

namespace SchoolManager.Services.Interfaces
{
    public interface ISubjectServices
    {
        Task<List<SubjectSummaryDto>> GetAllAsync();
        Task<SubjectSummaryDto?> GetSubjectByIdAsync(Guid id);
        Task<Subject> AddSubjectAsync(AddSubjectDto addSubjectDto);
        Task<bool> UpdateSubjectAsync(Guid id, UpdateSubjectDto updateStudentDto);
        Task<bool> DeleteSubjectAsync(Guid id);
    }
}
