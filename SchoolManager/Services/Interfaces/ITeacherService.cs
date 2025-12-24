using SchoolManager.Models.Dtos.Teacher;
using SchoolManager.Models.Entities;

namespace SchoolManager.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<List<Teacher>> GetAllAsync();
        Task<Teacher?> GetStudentByIdAsync(Guid id);
        Task<Teacher> AddTeacherAsync(AddTeacherDto addTeacherDto);
        Task<bool> UpdateTeacherAsync(Guid id, UpdateTeacherDto updateTeacherDto);
        Task<bool> DeleteTeacherAsync(Guid id);
    }
}
