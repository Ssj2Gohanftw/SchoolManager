using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Student;
using SchoolManager.Models;

namespace SchoolManager.Data.Repositories.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<PagedResults<Student>> GetPagedAsync(StudentQueryDto studentQueryDto);
        Task<List<Student>> GetHobbies();
    }
}
