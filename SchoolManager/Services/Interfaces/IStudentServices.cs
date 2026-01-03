using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Student;
using SchoolManager.Models.Entities;

namespace SchoolManager.Services.Interfaces
{
    public interface IStudentServices
    {
        Task<List<StudentDto>> GetAllAsync();
        Task<Student?> GetStudentByIdAsync(Guid id);
        Task<Student?> AddStudentAsync(AddStudentDto addStudentDto);
        Task<bool> UpdateStudentAsync(Guid id, UpdateStudentDto updateStudentDto);
        Task<bool> DeleteStudentAsync(Guid id);
        Task<PagedResults<StudentDetailsDto>> GetPagedStudentsAsync(StudentQueryDto studentQueryDto);
    }
}
