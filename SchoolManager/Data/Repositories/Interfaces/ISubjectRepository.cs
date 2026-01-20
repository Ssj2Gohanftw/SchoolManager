using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Subject;
using SchoolManager.Models;

namespace SchoolManager.Data.Repositories.Interfaces
{
    public interface ISubjectRepository : IRepository<Subject>
    {
        Task<PagedResults<Subject>> GetPagedResults(SubjectQueryDto subjectQueryDto);
    }
}
