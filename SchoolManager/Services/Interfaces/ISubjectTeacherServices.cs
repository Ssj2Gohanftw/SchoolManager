using SchoolManager.Dtos.SubjectTeacher;

namespace SchoolManager.Services.Interfaces
{
    public interface ISubjectTeacherServices
    {
        Task AssignAsync(AddSubjectTeacherDto dto);
        Task<bool> UnassignAsync(DeleteSubjectTeacherDto dto);

        Task<List<SubjectTeacherDto>> GetAssignmentsForTeacherAsync(Guid teacherId);
        Task<List<SubjectTeacherDto>> GetAssignmentsForClassAsync(Guid classId);
        Task<List<SubjectTeacherDto>> GetAssignmentsForSubjectAsync(Guid subjectId);
    }
}
