using SchoolManager.Dtos.Subject;
using SchoolManager.Dtos.SubjectClass;
using SchoolManager.Models;

namespace SchoolManager.Services.Interfaces
{
    public interface ISubjectClassServices
    {
        //Task<List<SubjectClassDto>> AssignSubjects(AddSubjectClassDto addSubjectClassDto);
        Task<List<SubjectSummaryDto>> GetAssignmentDetailsForClassAsync(Guid classId);
        Task<List<SubjectClass>> AssignSubjects(AddSubjectClassDto addSubjectClassDto);
    }
}
