using SchoolManager.Models.Dtos.Common;
using SchoolManager.Models.Dtos.Student;
using SchoolManager.Models.Entities;

namespace SchoolManager.Services.Interfaces
{
    public interface IStudentServices
    {
        Task<List<StudentDto>> GetAllAsync();
        Task<List<StudentDto>> GetAllSortedAsync();
        Task<Student?> GetStudentByIdAsync(Guid id);
        Task<Student?> AddStudentAsync(AddStudentDto addStudentDto);
        Task<bool> UpdateStudentAsync(Guid id, UpdateStudentDto updateStudentDto);
        Task<bool> DeleteStudentAsync(Guid id);
        Task<PagedResults<StudentDto>> GetPagedStudentsAsync(StudentQueryDto studentQueryDto);
    }
}
