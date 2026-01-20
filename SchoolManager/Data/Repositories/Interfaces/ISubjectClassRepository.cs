using SchoolManager.Models;

namespace SchoolManager.Data.Repositories.Interfaces
{
    public interface ISubjectClassRepository:IRepository<SubjectClass>
    {
        //Task<bool> Exists(SubjectClass subjectClass);
        Task AssignSubjectsToClass(List<Guid> subjectIds,Guid classId);
            //IReadOnlyCollection<Guid> subjectIds); 
            //Guid classId);
    }
}
