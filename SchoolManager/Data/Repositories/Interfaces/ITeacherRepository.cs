using SchoolManager.Models.Dtos.Common;
using SchoolManager.Models.Dtos.Teacher;
using SchoolManager.Models.Entities;

namespace SchoolManager.Data.Repositories.Interfaces
{
    public interface ITeacherRepository
    {
        Task AddAsync(Teacher teacher);
        Task<List<Teacher>> GetAllAsync();
        Task<Teacher?> GetByIdAsync(Guid id);
        Task<Teacher?> GetByIdWithAssignmentsAsync(Guid id);
        Task<bool> Remove(Teacher teacher);
        Task<bool> Update(Teacher teacher);
        Task<PagedResults<Teacher>> GetPagedAsync(TeacherQueryDto teacherQueryDto);

    }
}
