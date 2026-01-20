using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Student;
using SchoolManager.Models;

namespace SchoolManager.Services.Interfaces
{
    public interface IStudentServices
    {
        Task<List<StudentDto>> GetAllAsync();
        Task<Student?> GetStudentByIdAsync(Guid id);
        Task<Student?> AddStudentAsync(AddStudentDto addStudentDto);
        Task<bool> UpdateStudentAsync(Guid id, UpdateStudentDto updateStudentDto);
        Task<bool> DeleteStudentAsync(Guid id);
        Task<bool> AssignStudentToClassAsync(Guid studentId, AssignStudentClassDto assignStudentClassDto);
        Task<PagedResults<StudentDetailsDto>> GetPagedStudentsAsync(StudentQueryDto studentQueryDto);

        Task<List<StudentHobbiesDto>> GetStudentHobbies();

        //Task<>
    }
}
