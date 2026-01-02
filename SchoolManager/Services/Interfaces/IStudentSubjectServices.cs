using SchoolManager.Dtos.StudentSubject;
using SchoolManager.Models.Entities;

namespace SchoolManager.Services.Interfaces
{
    public interface IStudentSubjectServices
    {
        Task AssignSubjectToStudentAsync(AddStudentSubjectDto dto);
        Task<List<Subject>> GetSubjectsForStudentAsync(Guid studentId);
        Task<bool> RemoveSubjectFromStudentAsync(DeleteStudentSubjectDto dto);
        Task AssignSubjectsToClassAsync(Guid classId, AssignSubjectsToClassDto dto);
    }
}
