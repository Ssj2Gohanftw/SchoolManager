using SchoolManager.Models.Dtos.Common;
using SchoolManager.Models.Dtos.Student;
using SchoolManager.Models.Entities;

namespace SchoolManager.Data.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task AddAsync(Student student);
        Task<List<Student>> GetAllAsync();
        Task<List<Student>> GetAllSortedAsync();
        Task<Student?> GetByIdAsync(Guid id);
        Task<bool> Remove(Student student);
        Task<bool> Update(Student student);
        Task<PagedResults<Student>> GetPagedAsync(StudentQueryDto studentQueryDto);
    }
}
