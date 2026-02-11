using SchoolManager.Models;

namespace SchoolManager.Data.Repositories.Interfaces
{
    public interface ISubjectClassRepository:IRepository<SubjectClass>
    {
        //Task<bool> Exists(SubjectClass subjectClass);
        Task<List<Guid>> GetExistingSubAssignmentsForClass(Guid classId);
        Task<List<SubjectClass>> GetAllAssignmentDetailsForClass(Guid classId);
        //Task AssignSubjectsToClass(List<Guid> subjectIds,Guid classId);
        //IReadOnlyCollection<Guid> subjectIds); 
        //Guid classId);
        //public Task<List<SubjectClass>> AssignSubjectsToClass(List<Guid> subjectIds, Guid classId);
        //public Task<List<SubjectClass>> AssignSubjectsToClass(List<Guid> subjectIds, Guid classId);

    }
}
