using SchoolManager.Models.Dtos.Teacher;
using SchoolManager.Models.Entities;

namespace SchoolManager.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Teacher?> GetStudentByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Teacher> AddTeacherAsync(AddTeacherDto addTeacherDto, CancellationToken cancellationToken = default);
        Task<bool> UpdateTeacherAsync(Guid id, UpdateTeacherDto updateTeacherDto, CancellationToken cancellationToken = default);
        Task<bool> DeleteTeacherAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
