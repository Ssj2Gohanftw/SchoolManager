using SchoolManager.Models;

namespace SchoolManager.Data.Repositories.Interfaces
{
    public interface ISubjectTeacherRepository: IRepository<SubjectTeacher>
    {
        //Task AddAsync(SubjectTeacher subjectTeacher);
        Task<bool> Exists(SubjectTeacher subjectTeacher);
        Task<List<SubjectTeacher>> GetAssignmentsForTeacher(Guid teacherId);
        Task<List<SubjectTeacher>> GetAssignmentsForClass(Guid classId);
        Task<List<SubjectTeacher>> GetAssignmentsForSubject(Guid subjectId);
    }
}
