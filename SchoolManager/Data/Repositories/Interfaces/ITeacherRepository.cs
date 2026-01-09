using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Teacher;
using SchoolManager.Models.Entities;

namespace SchoolManager.Data.Repositories.Interfaces
{
    public interface ITeacherRepository:IRepository<Teacher>
    {
        Task<PagedResults<Teacher>> GetPagedAsync(TeacherQueryDto teacherQueryDto);

    }
}
