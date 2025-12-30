using SchoolManager.Models.Dtos.Common;
using SchoolManager.Models.Dtos.Subject;
using SchoolManager.Models.Entities;

namespace SchoolManager.Data.Repositories.Interfaces
{
    public interface ISubjectRepository
    {
        Task AddAsync(Subject subject);
        Task<List<Subject>> GetAllAsync();
        Task<Subject?> GetByIdAsync(Guid id);
        Task<bool> Remove(Subject subject);
        Task<bool> Update(Subject subject);
        Task<PagedResults<Subject>> GetPagedResults(SubjectQueryDto subjectQueryDto);
        

    }
}
